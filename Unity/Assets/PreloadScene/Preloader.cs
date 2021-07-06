using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Localization;
using ResourceLoader;

namespace PreloadScene
{
    /// <summary>
    /// ゲーム開始前の読み込みクラス
    /// </summary>
    public class Preloader: MonoBehaviour
    {
        public void Start()
        {
            // ゲームの実行されているディレクトリを取得する
#if UNITY_EDITOR
            var filePath = Directory.GetCurrentDirectory();
#else
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\");
#endif

            // とりあえず仮で英語の言語設定にしておく
            var jsonFilePath = string.Format("{0}\\Languages\\en-US.json", filePath);
            var jsonReader = new ResourceLoader.TextLoader(jsonFilePath);
            LocalizeLoader.Instance.Initialize(new LocalizeAnalyzer(jsonReader));
            LocalizeLoader.Instance.SetLocale("en-US");

            var sqlserver = new SQLiteServer.SQLiteServer();
            sqlserver.Start(filePath, "game.db");

            //UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}