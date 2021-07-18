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
    /// ゲーム開始前の読み込みクラス
    /// </summary>
    public class Preloader: MonoBehaviour
    {
        public void Start()
        {
            // とりあえず仮で英語の言語設定にしておく
            var jsonFilePath = string.Format("{0}\\Languages\\en-US.json", Constant.Path.WorkingDirectory).Replace('\\', '/');
            var jsonReader = new TextLoader(jsonFilePath);
            LocalizeLoader.Instance.Initialize(new LocalizeAnalyzer(jsonReader));
            LocalizeLoader.Instance.Locale = "en-US";

            var sqlserver = new SQLiteServer();
            sqlserver.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

            // プレイヤー登録
            // スキーマ的には複数人登録できるが、しばらくはプレイヤー切り替え機能は実装しない
            var players = sqlserver.InstantiateNewQueryBuilder().Table("players").Select("*").Execute<Player>();
            if(players.RecordCount == 0)
            {
                // 最初の起動時にはプレイヤー登録をする
                sqlserver.InstantiateNewQueryBuilder().Table("players").Insert(null, Guid.NewGuid().ToString(), Constant.SQLite.DefaultPlayerName).Execute();
            }

            sqlserver.Close();

            UnityEngine.SceneManagement.SceneManager.LoadScene("ChartLoadScene");
        }
    }
}