using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SQLiteUnity;

namespace SQLiteServer
{
    /// <summary>
    /// SQLite�f�[�^�x�[�X���T�[�o�I�ɊǗ�����N���X
    /// </summary>
    public class SQLiteServer
    {
        private SQLite sqLiteInstance;

        /// <summary>
        /// �f�[�^�x�[�X�T�[�o���N������
        /// </summary>
        /// <param name="databaseInstanceDestination">SQLite�̃f�[�^�x�[�X�C���X�^���X�����݂���f�B���N�g��</param>
        /// <param name="databaseName">�f�[�^�x�[�X��</param>
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
                    // �V�K�쐬���鎞�A�}�C�O���[�V�����̗����e�[�u�������쐬����
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
    }
}