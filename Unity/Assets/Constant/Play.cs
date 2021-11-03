using System.Collections;
using UnityEngine;

namespace Constant
{
    /// <summary>
    /// ゲームプレイに関する定数
    /// </summary>
	public static class Play
	{
		/// <summary>
        /// 最大のスクロールスピード
        /// </summary>
        public static int FastestScrollSpeed
        {
            get
            {
                return 1000;
            }
        }

        /// <summary>
        /// 最低のスクロールスピード
        /// </summary>
        public static int SlowestScrollSpeed
        {
            get
            {
                return 25;
            }
        }

        /// <summary>
        /// スクロールスピードの単位
        /// </summary>
        public static int ScrollSpeedUnit
        {
            get
            {
                return 25;
            }
        }
	}
}