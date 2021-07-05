using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// ローカライズ情報クラス
    /// </summary>
    public class Localize
    {
        private string locale;
        private Dictionary<string, string> lexicon;

        /// <summary>
        /// ロケール情報を取得する
        /// </summary>
        public string Locale
        {
            get
            {
                return locale;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="locale"></param>
        public Localize(string locale)
        {
            this.locale = locale;

            lexicon = new Dictionary<string, string>();
        }

        /// <summary>
        /// ローカライズ情報を追加する
        /// </summary>
        /// <param name="key">取得する際のキー</param>
        /// <param name="replacement">置き換える文字列</param>
        public void AddLexicon(string key, string replacement)
        {
            lexicon.Add(key, replacement);
        }

        /// <summary>
        /// ローカライズ情報を探す
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Find(string key)
        {
            if(key == null || !lexicon.ContainsKey(key))
            {
                return "";
            }

            return lexicon[key];
        }
    }
}