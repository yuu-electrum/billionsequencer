using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "chart_profiles"テーブルのモデル
    /// </summary>
    public class ChartProfiles
    {
        private SQLiteRow origin;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="record">レコード</param>
        public ChartProfiles(SQLiteRow record)
        {
            origin = record;
        }

        public int Id
        {
            get
            {
                return origin.GetColumn<int>("id", -1);
            }
        }

        public string ChartHash
        {
            get
            {
                return origin.GetColumn<string>("chart_hash", "");
            }
        }

        public string Title
        {
            get
            {
                return origin.GetColumn<string>("title", "");
            }
        }

        public string Artist
        {
            get
            {
                return origin.GetColumn<string>("artist", "");
            }
        }

        public int LaneCount
        {
            get
            {
                return origin.GetColumn<int>("lane_count", 1);
            }
        }

        public int Level
        {
            get
            {
                return origin.GetColumn<int>("level", 0);
            }
        }

        public double Bpm
        {
            get
            {
                return origin.GetColumn<double>("bpm", 0.0);
            }
        }

        public string SequenceDesigner
        {
            get
            {
                return origin.GetColumn<string>("sequence_designer", "");
            }
        }
    }
}