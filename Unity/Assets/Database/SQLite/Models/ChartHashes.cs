using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "chart_hashes"テーブルのモデル
    /// </summary>
    public class ChartHashes
    {
        private SQLiteRow origin;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="record">レコード</param>
        public ChartHashes(SQLiteRow record)
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
    }
}