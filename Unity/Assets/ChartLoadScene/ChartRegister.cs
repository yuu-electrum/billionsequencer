using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ResourceLoader;
using SQLiteManagement;
using ChartManagement;
using Database.SQLite.Models;

namespace ChartLoadScene
{
    /// <summary>
    /// 譜面を登録するクラス
    /// </summary>
    public class ChartRegister
    {
        private Sha256FileHashCalcurator hashCalcurator;
        private SQLiteServer server;

        /// <summary>
        /// 登録結果
        /// </summary>
        public enum RegistrationResult
        {
            /// <summary>
            /// 登録完了
            /// </summary>
            Done,

            /// <summary>
            /// すでに登録されている
            /// </summary>
            AlreadyRegistered,

            /// <summary>
            /// 必要事項未入力
            /// </summary>
            UnfulfilledProfile,

            /// <summary>
            /// 譜面のフォーマットが不正
            /// </summary>
            IllegalFormat
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hashCalcurator">ハッシュを計算するクラス</param>
        /// <param name="server">SQLiteサーバ</param>
        public ChartRegister(Sha256FileHashCalcurator hashCalcurator, SQLiteServer server)
        {
            this.hashCalcurator = hashCalcurator;
            this.server = server;
        }

        public RegistrationResult Register(string chartFilePath)
        {
            // 譜面ファイルのSHA256ハッシュを計算する
            var hash = hashCalcurator.Calcurate(new TextLoader(chartFilePath));
            var existingHashRecord = server.InstantiateNewQueryBuilder().Table("chart_hashes").Select("*").Where("chart_hash", "=", hash).Execute<ChartHash>();
            if(existingHashRecord.RecordCount == 1)
            {
                return RegistrationResult.AlreadyRegistered;
            }

            Chart chart;
            try
            {
                chart = new ChartAnalyzer(new TextLoader(chartFilePath)).Analyze();
            }
            catch(Exception e)
            {
                Debug.LogWarningFormat("Unexpected error occurred when loading a chart {0}. Detail: {1}", chartFilePath, e.ToString());
                return RegistrationResult.IllegalFormat;
            }

            var title     = chart.GlobalConfigurations.Title;
            var artist    = chart.GlobalConfigurations.Artist;
            var laneCount = chart.GlobalConfigurations.LaneCount;
            var level     = chart.GlobalConfigurations.Level;

            var bpmTransitions = new List<double>();
            foreach(var transition in chart.BpmTransitions)
            {
                bpmTransitions.Add(transition.Bpm);
            }
            bpmTransitions.Sort((a, b) => Math.Sign(a - b));
            if(bpmTransitions.Count == 0)
            {
                // 譜面データが不正
                return RegistrationResult.IllegalFormat;
            }

            var minBpm = bpmTransitions.First();
            var maxBpm = bpmTransitions.Last();
            var sequenceDesigner = chart.GlobalConfigurations.SequenceDesigner;

            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(artist))
            {
                // タイトルかアーティストが未入力
                return RegistrationResult.UnfulfilledProfile;
            }

            var isBpmUndefined = Math.Abs(maxBpm - 0.0) < double.Epsilon && Math.Abs(minBpm - 0.0) < double.Epsilon;
            if(laneCount == 0 || isBpmUndefined)
            {
                // 譜面データが不正
                return RegistrationResult.IllegalFormat;
            }

            // 登録されていないなら譜面を登録する
            server.InstantiateNewQueryBuilder().Table("chart_hashes").Insert(null, hash).Execute();
            server.InstantiateNewQueryBuilder().Table("chart_profiles").Insert(
                null,
                hash,
                title,
                artist,
                laneCount.ToString(),
                level.ToString(),
                minBpm.ToString(),
                maxBpm.ToString(),
                sequenceDesigner
            ).Execute();
            return RegistrationResult.Done;
        }
    }
}