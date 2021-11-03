using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;
using SQLiteManagement.Migrations;

namespace SQLiteManagement
{
    /// <summary>
    /// SQLiteデータベースに対してマイグレーションを実行するクラス
    /// </summary>
    public class SQLiteMigrator
    {
        private SQLite sqLiteInstance;
        private List<IMigration> migrations;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="instance"></param>
        public SQLiteMigrator(SQLite instance)
        {
            sqLiteInstance = instance;
            migrations = new List<IMigration>();

            // マイグレーションを追加していく
            migrations.Add(new InitialMigration());

            // マイグレーションを、優先度が昇順になるように並び替える
            migrations.Sort((a, b) => b.Id - a.Id);
        }

        /// <summary>
        /// マイグレーションを実行する
        /// </summary>
        public void Execute()
        {
            // マイグレーション履歴を参照して、すでに実行されたマイグレーションをスキップしつつ実行する
            if(sqLiteInstance == null)
            {
                Debug.LogError("A SQLite instance is null for any reason and the migration is aborted.");
                return;
            }

            var latestMigrationHistory = sqLiteInstance.ExecuteQuery("SELECT MAX(id) AS latest_migration_id FROM migration_history");
            var latestMigrationId = latestMigrationHistory.Rows[0].GetColumn<int>("latest_migration_id", int.MinValue);

            foreach(var migration in migrations)
            {
                if(migration.Id > latestMigrationId)
                {
                    try
                    {
                        sqLiteInstance.TransactionQueries(migration.ExecutionQuery);
                        sqLiteInstance.TransactionQueries(string.Format("INSERT INTO migration_history VALUES ({0})", migration.Id));
                        latestMigrationId = migration.Id;
                    }
                    catch(SQLiteException e)
                    {
                        Debug.LogErrorFormat("Failed to migration due to an error. Detail: {0}" + e.ToString());
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// マイグレーション履歴のテーブルを作成するクエリ文を取得する
        /// </summary>
        public static string MigrationHistoryCreateQuery
        {
            get
            {
                return$@"
PRAGMA foreign_keys=true;
CREATE TABLE migration_history(id INTEGER);
";
            }
        }
    }
}