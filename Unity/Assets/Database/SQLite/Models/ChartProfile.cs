using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "chart_profiles"テーブルのモデル
    /// </summary>
    public class ChartProfile
    {
        private SQLiteRow origin;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="record">レコード</param>
        public ChartProfile(SQLiteRow record)
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
                if(!origin.ContainsKey("chart_hash"))
                {
                    return "";
                }

                return origin["chart_hash"].ToString();
            }
        }

        public string Title
        {
            get
            {
                if(!origin.ContainsKey("title"))
                {
                    return "";
                }

                return origin["title"].ToString();
            }
        }

        public string Artist
        {
            get
            {
                if(!origin.ContainsKey("artist"))
                {
                    return "";
                }

                return origin["artist"].ToString();
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

        public double MinBpm
        {
            get
            {
                return origin.GetColumn<double>("min_bpm", 0.0);
            }
        }

        public double MaxBpm
        {
            get
            {
                return origin.GetColumn<double>("max_bpm", 0.0);
            }
        }

        public string SequenceDesigner
        {
            get
            {
                if(!origin.ContainsKey("sequence_designer"))
                {
                    return "";
                }

                return origin["sequence_designer"].ToString();
            }
        }
    }
}