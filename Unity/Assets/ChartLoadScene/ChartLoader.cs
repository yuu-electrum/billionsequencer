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
    /// ���ʂ�ǂݍ��ރN���X
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
            var fileEnumrator = directoryInfo.EnumerateFiles();
            server = new SQLiteServer();
            server.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

            loadingFinished = false;
            loadingFinished = await LoadChartsInDirectoryAsync(fileEnumrator, new ChartRegister(new Sha256FileHashCalcurator(), server));

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
        /// ���ʂ�ǂݍ���
        /// </summary>
        /// <param name="fileEnumerator">�t�@�C����񋓂���Enumerator</param>
        /// <param name="hashCalcurator">�n�b�V�����v�Z����N���X</param>
        /// <param name="register">�t�@�C����o�^����N���X</param>
        /// <returns></returns>
        public async UniTask<bool> LoadChartsInDirectoryAsync(IEnumerable<FileInfo> fileEnumerator, ChartRegister register)
        {
            foreach(var file in fileEnumerator)
            {
                currentLoadingFilePath = file.FullName;
                var result = register.Register(file.FullName);
                if(result != ChartRegister.RegistrationResult.Done && result == ChartRegister.RegistrationResult.IllegalFormat)
                {
                    Debug.LogErrorFormat("Chart in {0} was not registered due to its illegal format.", file.FullName, result.ToString());
                }
            }

            return true;
        }
    }
}