using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
            var filePath = Directory.GetCurrentDirectory().Replace("\\", "/");
#else
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\").Replace("\\", "/");
#endif

            // とりあえず仮で英語の言語設定にしておく
            Localization.LocalizeLoader.Instance.Load(string.Format("{0}/Languages/en-US.json", filePath));
            Localization.LocalizeLoader.Instance.SetLocale("en-US");

            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}