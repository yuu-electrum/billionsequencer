using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constant
{
    /// <summary>
    /// SQLiteに関する定数
    /// </summary>
    public static class SQLite
    {
        /// <summary>
        /// デフォルトのデータベースファイル名
        /// </summary>
        public static string DatabaseInstanceFileName
        {
            get
            {
                return "game.db";
            }
        }

        /// <summary>
        /// デフォルトのプレイヤー名
        /// </summary>
        public static string DefaultPlayerName
        {
            get
            {
                return "Sayoko Takayama";
            }
        }
    }
}