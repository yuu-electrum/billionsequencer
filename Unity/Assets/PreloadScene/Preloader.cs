using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Localization;
using ResourceLoader;
using Database.SQLite;
using Database.SQLite.Models;
using SQLiteManagement;

namespace PreloadScene
{
    /// <summary>
    /// �Q�[���J�n�O�̓ǂݍ��݃N���X
    /// </summary>
    public class Preloader: MonoBehaviour
    {
        public void Start()
        {
            // �Ƃ肠�������ŉp��̌���ݒ�ɂ��Ă���
            var jsonFilePath = string.Format("{0}\\Languages\\en-US.json", Constant.Path.WorkingDirectory);
            var jsonReader = new TextLoader(jsonFilePath);
            LocalizeLoader.Instance.Initialize(new LocalizeAnalyzer(jsonReader));
            LocalizeLoader.Instance.Locale = "en-US";

            var sqlserver = new SQLiteServer();
            sqlserver.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

            // �v���C���[�o�^
            // �X�L�[�}�I�ɂ͕����l�o�^�ł��邪�A���΂炭�̓v���C���[�؂�ւ��@�\�͎������Ȃ�
            var players = sqlserver.InstantiateNewQueryBuilder().Table("players").Select("*").Execute<Players>();
            if(players.RecordCount == 0)
            {
                // �ŏ��̋N�����ɂ̓v���C���[�o�^������
                sqlserver.InstantiateNewQueryBuilder().Table("players").Insert(null, Guid.NewGuid().ToString(), "sayoko_takayama").Execute();
            }

            sqlserver.Close();

            UnityEngine.SceneManagement.SceneManager.LoadScene("ChartLoadScene");
        }
    }
}