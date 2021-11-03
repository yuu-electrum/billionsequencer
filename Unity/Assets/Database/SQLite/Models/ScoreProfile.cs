using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "chart_profiles"テーブルのモデル
    /// </summary>
    public class ScoreProfile
    {
        private SQLiteRow origin;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="record">レコード</param>
        public ScoreProfile(SQLiteRow record)
        {
            origin = record;
        }

        public string Guid
        {
            get
            {
                if(!origin.ContainsKey("guid"))
                {
                    return "";
                }

                return origin["guid"].ToString();
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

        public string PlayResult
        {
            get
            {
                if(!origin.ContainsKey("play_result"))
                {
                    return "";
                }

                return origin["play_result"].ToString();
            }
        }

        public int Score
        {
            get
            {
                if(!origin.ContainsKey("score"))
                {
                    return 0;
                }

                return int.Parse(origin["score"].ToString());
            }
        }
    }
}