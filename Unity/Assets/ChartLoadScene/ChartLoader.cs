using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SQLiteManagement;
using ChartManagement;
using ResourceLoader;
using TMPro;

namespace ChartLoadScene
{
    /// <summary>
    /// 譜面を読み込むクラス
    /// </summary>
    public class ChartLoader: MonoBehaviour
    {
        private bool loadingFinished;
        private SQLiteServer server;
        private string lastLoadedFilePath;
        private string currentLoadingFilePath;

        [SerializeField]
        private TextMeshProUGUI loadingFilePath;

        public async void Start()
        {
            var directoryInfo = new DirectoryInfo(Constant.Path.ChartDirectory);
            var filePathEnumrator = Directory.EnumerateFiles(Constant.Path.ChartDirectory, "*.json", SearchOption.AllDirectories);
            server = new SQLiteServer();
            server.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

            loadingFinished = false;
            loadingFinished = await LoadChartsInDirectoryAsync(filePathEnumrator, new ChartRegister(new Sha256FileHashCalcurator(), server));

            lastLoadedFilePath = "";
            currentLoadingFilePath = "";
        }

        public void Update()
        {
            if(currentLoadingFilePath != lastLoadedFilePath)
            {
                loadingFilePath.text = currentLoadingFilePath;
            }

            if(loadingFinished)
            {
                loadingFilePath.text = "";
                server.Close();
                UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
            }
        }

        /// <summary>
        /// 譜面を読み込む
        /// </summary>
        /// <param name="fileEnumerator">ファイルを列挙するEnumerator</param>
        /// <param name="hashCalcurator">ハッシュを計算するクラス</param>
        /// <param name="register">ファイルを登録するクラス</param>
        /// <returns></returns>
        public async UniTask<bool> LoadChartsInDirectoryAsync(IEnumerable<string> fileEnumerator, ChartRegister register)
        {
            foreach(var file in fileEnumerator)
            {
                currentLoadingFilePath = file.Replace('\\', '/');
                var result = register.Register(currentLoadingFilePath);
                if(result != ChartRegister.RegistrationResult.Done && result == ChartRegister.RegistrationResult.IllegalFormat)
                {
                    Debug.LogErrorFormat("Chart in {0} was not registered due to its illegal format.", file, result.ToString());
                }
            }

            return true;
        }
    }
}