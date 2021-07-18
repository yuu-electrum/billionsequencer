using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Constant
{
    /// <summary>
    /// パスに関する定数
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// ゲームを実行しているディレクトリを取得する
        /// </summary>
        public static string WorkingDirectory
        {
            get
            {
#if UNITY_EDITOR
                return Directory.GetCurrentDirectory();
#else
                return System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\");
#endif
            }
        }

        /// <summary>
        /// 譜面ファイルが格納されているディレクトリを取得する
        /// </summary>
        public static string ChartDirectory
        {
            get
            {
                return WorkingDirectory + "//Charts";
            }
        }
    }
}