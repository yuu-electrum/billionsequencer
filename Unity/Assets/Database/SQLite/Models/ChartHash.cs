using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "chart_hashes"�e�[�u���̃��f��
    /// </summary>
    public class ChartHash
    {
        private SQLiteRow origin;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="record">���R�[�h</param>
        public ChartHash(SQLiteRow record)
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

        public string Hash
        {
            get
            {
                return origin.GetColumn<string>("chart_hash", "");
            }
        }
    }
}