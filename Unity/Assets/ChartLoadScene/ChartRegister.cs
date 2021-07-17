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
    /// ���ʂ�o�^����N���X
    /// </summary>
    public class ChartRegister
    {
        private Sha256FileHashCalcurator hashCalcurator;
        private SQLiteServer server;

        /// <summary>
        /// �o�^����
        /// </summary>
        public enum RegistrationResult
        {
            /// <summary>
            /// �o�^����
            /// </summary>
            Done,

            /// <summary>
            /// ���łɓo�^����Ă���
            /// </summary>
            AlreadyRegistered,

            /// <summary>
            /// �K�v����������
            /// </summary>
            UnfulfilledProfile,

            /// <summary>
            /// ���ʂ̃t�H�[�}�b�g���s��
            /// </summary>
            IllegalFormat
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="hashCalcurator">�n�b�V�����v�Z����N���X</param>
        /// <param name="server">SQLite�T�[�o</param>
        public ChartRegister(Sha256FileHashCalcurator hashCalcurator, SQLiteServer server)
        {
            this.hashCalcurator = hashCalcurator;
            this.server = server;
        }

        public RegistrationResult Register(string chartFilePath)
        {
            // ���ʃt�@�C����SHA256�n�b�V�����v�Z����
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
                // ���ʃf�[�^���s��
                return RegistrationResult.IllegalFormat;
            }

            var minBpm = bpmTransitions.First();
            var maxBpm = bpmTransitions.Last();
            var sequenceDesigner = chart.GlobalConfigurations.SequenceDesigner;

            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(artist))
            {
                // �^�C�g�����A�[�e�B�X�g��������
                return RegistrationResult.UnfulfilledProfile;
            }

            var isBpmUndefined = Math.Abs(maxBpm - 0.0) < double.Epsilon && Math.Abs(minBpm - 0.0) < double.Epsilon;
            if(laneCount == 0 || isBpmUndefined)
            {
                // ���ʃf�[�^���s��
                return RegistrationResult.IllegalFormat;
            }

            // �o�^����Ă��Ȃ��Ȃ畈�ʂ�o�^����
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