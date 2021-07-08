using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;
using SQLiteManagement.Migrations;

namespace SQLiteManagement
{
    /// <summary>
    /// SQLite�f�[�^�x�[�X�ɑ΂��ă}�C�O���[�V���������s����N���X
    /// </summary>
    public class SQLiteMigrator
    {
        private SQLite sqLiteInstance;
        private List<IMigration> migrations;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="instance"></param>
        public SQLiteMigrator(SQLite instance)
        {
            sqLiteInstance = instance;
            migrations = new List<IMigration>();

            // �}�C�O���[�V������ǉ����Ă���
            migrations.Add(new InitialMigration());

            // �}�C�O���[�V�������A�D��x�������ɂȂ�悤�ɕ��ёւ���
            migrations.Sort((a, b) => b.Id - a.Id);
        }

        /// <summary>
        /// �}�C�O���[�V���������s����
        /// </summary>
        public void Execute()
        {
            // �}�C�O���[�V�����������Q�Ƃ��āA���łɎ��s���ꂽ�}�C�O���[�V�������X�L�b�v�����s����
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
        /// �}�C�O���[�V���������̃e�[�u�����쐬����N�G�������擾����
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