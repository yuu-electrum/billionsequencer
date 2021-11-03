using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SQLiteManagement;
using ChartManagement;
using Database.SQLite.Models;
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
            loadingFinished = await LoadChartsInDirectoryAsync(new Sha256FileHashCalcurator(), filePathEnumrator, new ChartRegister(server));

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
        /// <param name="hashCalcurator">ハッシュを計算するクラス</param>
        /// <param name="fileEnumerator">ファイルを列挙するEnumerator</param>
        /// <param name="register">ファイルを登録するクラス</param>
        /// <returns></returns>
        public async UniTask<bool> LoadChartsInDirectoryAsync(IFileHashCalcurator hashCalcurator, IEnumerable<string> fileEnumerator, ChartRegister register)
        {
            var registeredChartProfiles = server.InstantiateNewQueryBuilder()
                .Table("chart_profiles")
                .Select("*")
                .Execute<ChartProfile>()
                .Records;
            var chartAnalyzers = new List<ChartAnalyzer>();

            foreach(var registeredChartProfile in registeredChartProfiles)
            {
                var analyzer = new ChartAnalyzer(hashCalcurator, new TextLoader(registeredChartProfile.FilePath));

                if(!File.Exists(analyzer.Path))
                {
                    // そのファイルパスが存在しない場合、登録されている譜面リストから削除する
                    server.InstantiateNewQueryBuilder().Table("chart_profiles").Delete("chart_hash", "=", registeredChartProfile.ChartHash).Execute();
                    continue;
                }

                chartAnalyzers.Add(analyzer);
            }

            foreach(var filePath in fileEnumerator)
            {
                var separatorConvertedFilePath = filePath.Replace('\\', '/');
                var textLoader = new TextLoader(separatorConvertedFilePath);
                var hash = hashCalcurator.Calcurate(textLoader);

                var identicalChartProfiles = server.InstantiateNewQueryBuilder()
                    .Table("chart_profiles")
                    .Select("*")
                    .Where("file_path", "=", separatorConvertedFilePath)
                    .AndWhere("chart_hash", "=", hash)
                    .Execute<ChartProfile>()
                    .Records;

                var modifiedChartProfile = server.InstantiateNewQueryBuilder()
                    .Table("chart_profiles")
                    .Select("*")
                    .Where("file_path", "=", separatorConvertedFilePath)
                    .AndWhere("chart_hash", "<>", hash)
                    .Execute<ChartProfile>()
                    .Records;

                foreach(var chartProfile in modifiedChartProfile)
                {
                    server.InstantiateNewQueryBuilder()
                        .Table("chart_profiles")
                        .Delete("chart_hash", "=", chartProfile.ChartHash)
                        .Execute();
                }

                if(identicalChartProfiles.Count == 0)
                {
                    // 登録されていない譜面の場合、新規登録を行う
                    register.Register(new ChartAnalyzer(hashCalcurator, textLoader));
                }

                var scoreProfile = server.InstantiateNewQueryBuilder()
                    .Table("score_profiles")
                    .Select("*")
                    .Where("chart_hash", "=", hash)
                    .Execute<ScoreProfile>();

                // ユーザは今のところ常に1人しかいない。
                // PlayerPrefsとかに保存しておくべきだろうか
                var player = server.InstantiateNewQueryBuilder()
                    .Table("players")
                    .Select("*")
                    .Execute<Player>();

                if(scoreProfile.RecordCount == 0)
                {
                    // 登録されていない場合は、スコアデータを用意する
                    server.InstantiateNewQueryBuilder()
                        .Table("score_profiles")
                        .Insert(player.Records.First().Guid, hash, "never_played", "0")
                        .Execute();
                }
            }
            return true;
        }
    }
}