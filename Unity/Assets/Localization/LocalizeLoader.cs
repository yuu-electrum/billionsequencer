using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;

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

        /// <summary>
        /// ローカライズ情報を読み込む
        /// </summary>
        /// <param name="path">ローカライズ情報が含まれるテキストファイル</param>
        /// <returns>成功時にtrue、失敗時にfalse</returns>
        public bool Load(string path)
        {
            if(path == null || !File.Exists(path))
            {
                Debug.LogFormat("A localization file {0} is not existing.", path);
                return false;
            }

            try
            {
                // JSONで記述された生のデータを読み込む
                var streamReader = new StreamReader(path);
                string raw = streamReader.ReadToEnd();
                streamReader.Close();

                // ローカライズ情報として読み込む
                var analyzer = new LocalizeAnalyzer(raw);
                var localize = analyzer.Analyze();

                localizations.Add(localize.Locale, localize);
            }
            catch(Exception e)
            {
                Debug.LogFormat("Failed to load a localization file at {0}. Detail: {1}", path, e.ToString());
            }

            return true;
        }

        /// <summary>
        /// ロケールを設定する
        /// </summary>
        /// <param name="locale">ロケールコード</param>
        public void SetLocale(string locale)
        {
            currentLocale = locale;
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
                Debug.LogFormat("A localization with key {0} in the current locale {1} is not found.", key, currentLocale);
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