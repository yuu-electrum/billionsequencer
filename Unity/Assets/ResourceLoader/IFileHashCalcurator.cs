using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ResourceLoader
{
    /// <summary>
    /// 指定されたファイルのハッシュ値を計算するインターフェース
    /// </summary>
    public interface IFileHashCalcurator
    {
        /// <summary>
        /// ファイルを読み込み、ハッシュ値を計算する
        /// </summary>
        /// <returns></returns>
        public string Calcurate(TextLoader textLoader);
    }
}