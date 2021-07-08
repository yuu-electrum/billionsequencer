using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "Players"�e�[�u���̃��f��
    /// </summary>
    public class Players
    {
        private SQLiteRow origin;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="record">���R�[�h</param>
        public Players(SQLiteRow record)
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

        public string Guid
        {
            get
            {
                return origin.GetColumn<string>("guid", "");
            }
        }

        public string PlayerName
        {
            get
            {
                return origin.GetColumn<string>("player_name", "sayoko_takayama");
            }
        }
    }
}