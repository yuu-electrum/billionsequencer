using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SQLiteManagement;
using ResourceLoader;
using Database.SQLite.Models;
using ChartManagement;

namespace ChartLoadScene
{
    /// <summary>
    /// ���ʂ�ǂݍ���
    /// </summary>
    public class ChartLoader: MonoBehaviour
    {
        private bool loadingFinished;
        private SQLiteServer server;

        public async void Start()
        {
            var directoryInfo = new DirectoryInfo(Constant.Path.ChartDirectory);
            var fileEnumrator = directoryInfo.EnumerateFiles();
            server = new SQLiteServer();
            server.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

            loadingFinished = false;
            loadingFinished = await LoadChartsInDirectoryAsync(fileEnumrator, new Sha256FileHashCalcurator(), server);
        }

        public void Update()
        {
            if(loadingFinished)
            {
                server.Close();
            }
        }

        /// <summary>
        /// ���ʂ�ǂݍ���
        /// </summary>
        /// <param name="fileEnumerator">�t�@�C����񋓂���Enumerator</param>
        /// <param name="hashCalcurator">�n�b�V�����v�Z����N���X</param>
        /// <param name="server">SQLite�T�[�o</param>
        /// <returns></returns>
        public async UniTask<bool> LoadChartsInDirectoryAsync(IEnumerable<FileInfo> fileEnumerator, Sha256FileHashCalcurator hashCalcurator, SQLiteServer server)
        {
            foreach(var file in fileEnumerator)
            {
                // ���ʃt�@�C����SHA256�n�b�V�����v�Z����
                var hash = hashCalcurator.Calcurate(new TextLoader(file.FullName));
                var existingHashRecord = server.InstantiateNewQueryBuilder().Table("chart_hashes").Select("*").Where("chart_hash", "=", hash).Execute<ChartHashes>();
                if(existingHashRecord.RecordCount == 0)
                {
                    // �o�^����Ă��Ȃ��n�b�V���Ȃ�o�^����
                    server.InstantiateNewQueryBuilder().Table("chart_hashes").Insert(null, hash).Execute();
                }

                var chartProfile = server.InstantiateNewQueryBuilder().Table("chart_profiles").Select("*").Where("chart_hash", "=", hash).Execute<ChartProfiles>();
                if(chartProfile.RecordCount == 0)
                {
                    // ���ʂ��o�^����Ă��Ȃ��Ȃ�o�^����
                    // TODO: ���ʐ���҈ȊO�̍��ڂ���Ȃ�G���[�ɂ���
                    var chart = new ChartAnalyzer(new TextLoader(file.FullName)).Analyze();

                    var title     = chart.GlobalConfigurations.Title;
                    var artist    = chart.GlobalConfigurations.Artist;
                    var laneCount = chart.GlobalConfigurations.LaneCount.ToString();
                    var level     = chart.GlobalConfigurations.Level.ToString();

                    var bpmTransitions = new List<double>();
                    foreach(var transition in chart.BpmTransitions)
                    {
                        bpmTransitions.Add(transition.Bpm);
                    }

                    bpmTransitions.Sort((a, b) => Math.Sign(a - b));

                    var minBpm = bpmTransitions.First().ToString();
                    var maxBpm = bpmTransitions.Last().ToString();
                    var sequenceDesigner = chart.GlobalConfigurations.SequenceDesigner;

                    server.InstantiateNewQueryBuilder().Table("chart_profiles").Insert(null, hash, title, artist, laneCount, level, minBpm, maxBpm, sequenceDesigner).Execute();
                }
            }

            return true;
        }
    }
}