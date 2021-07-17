using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite.Models
{
    /// <summary>
    /// "players"�e�[�u���̃��f��
    /// </summary>
    public class Player
    {
        private SQLiteRow origin;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="record">���R�[�h</param>
        public Player(SQLiteRow record)
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
                if(!origin.ContainsKey("guid"))
                {
                    return "";
                }

                return origin["guid"].ToString();
            }
        }

        public string PlayerName
        {
            get
            {
                if(!origin.ContainsKey("player_name"))
                {
                    return Constant.SQLite.DefaultPlayerName;
                }

                return origin["player_name"].ToString();
            }
        }
    }
}