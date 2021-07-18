using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SQLiteUnity;
using Database.SQLite;

namespace SQLiteManagement
{
    /// <summary>
    /// SQLiteデータベースをサーバ的に管理するクラス
    /// </summary>
    public class SQLiteServer
    {
        private SQLite sqLiteInstance;

        public SQLiteQueryBuilder InstantiateNewQueryBuilder()
        {
            return new SQLiteQueryBuilder(sqLiteInstance);
        }

        /// <summary>
        /// データベースサーバを起動する
        /// </summary>
        /// <param name="databaseInstanceDestination">SQLiteのデータベースインスタンスが存在するディレクトリ</param>
        /// <param name="databaseName">データベース名</param>
        public void Start(string databaseInstanceDestination, string databaseName)
        {
            var instanceFilePath = databaseInstanceDestination + "\\" + databaseName;
            try
            {
                if(File.Exists(instanceFilePath))
                {
                    sqLiteInstance = new SQLite(databaseName, null, databaseInstanceDestination);
                }
                else
                {
                    // 新規作成する時、マイグレーションの履歴テーブルだけ作成する
                    sqLiteInstance = new SQLite(databaseName, SQLiteMigrator.MigrationHistoryCreateQuery, databaseInstanceDestination);
                }
            }
            catch(ArgumentNullException exception)
            {
                Debug.LogErrorFormat("A SQLite server instance could not be found and created. Details: {0}", exception.ToString());
            }

            var migrator = new SQLiteMigrator(sqLiteInstance);
            migrator.Execute();
        }

        /// <summary>
        /// データベースサーバをシャットダウンする
        /// </summary>
        public void Close()
        {
            sqLiteInstance.Dispose();
        }
    }
}