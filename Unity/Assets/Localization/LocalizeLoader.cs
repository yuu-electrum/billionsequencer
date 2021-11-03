using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// ローカライズ情報を読み込むクラス
    /// </summary>
    public class LocalizeLoader
    {
        private static LocalizeLoader loaderInstance = null;
        private Dictionary<string, Localize> localizations;

        private string currentLocale = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private LocalizeLoader()
        {
            localizations = new Dictionary<string, Localize>();
        }

        /// <summary>
        /// クラスインスタンスを取得する
        /// </summary>
        public static LocalizeLoader Instance
        {
            get
            {
                if(loaderInstance == null)
                {
                    loaderInstance = new LocalizeLoader();
                }

                return loaderInstance;
            }
        }

        public void Initialize(LocalizeAnalyzer analyzer)
        {
            var localize = analyzer.Analyze();
            localizations.Add(localize.Locale, localize);
        }

        /// <summary>
        /// ロケールを取得または設定する
        /// </summary>
        /// <param name="locale">ロケールコード</param>
        public string Locale
        {
            set
            {
                currentLocale = value;
            }

            get
            {
                return currentLocale;
            }
        }

        /// <summary>
        /// ローカライズ情報を取得する
        /// </summary>
        /// <param name="key">ローカライズ情報のキー</param>
        /// <returns>ローカライズ文字列</returns>
        public string Find(string key)
        {
            if(currentLocale == null || !localizations.ContainsKey(currentLocale))
            {
                Debug.LogErrorFormat("A localization with key {0} in the current locale {1} is not found.", key, currentLocale);
                return "";
            }

            return localizations[currentLocale].Find(key);
        }

        /// <summary>
        /// ローカライズ情報を取得し、書式付き文字列が設定されている場合は渡された配列でそれを置き換える
        /// </summary>
        /// <param name="key">ローカライズ情報のキー</param>
        /// <param name="formatCorrespondingValues">書式付き文字列に対応するパラメータ</param>
        /// <returns></returns>
        public string Format(string key, params string[] formatCorrespondingValues)
        {
            return String.Format(Find(key), formatCorrespondingValues);
        }
    }
}