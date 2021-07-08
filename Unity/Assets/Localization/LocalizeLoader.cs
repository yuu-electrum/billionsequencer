using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// ���[�J���C�Y����ǂݍ��ރN���X
    /// </summary>
    public class LocalizeLoader
    {
        private static LocalizeLoader loaderInstance = null;
        private Dictionary<string, Localize> localizations;

        private string currentLocale = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        private LocalizeLoader()
        {
            localizations = new Dictionary<string, Localize>();
        }

        /// <summary>
        /// �N���X�C���X�^���X���擾����
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
        /// ���P�[�����擾�܂��͐ݒ肷��
        /// </summary>
        /// <param name="locale">���P�[���R�[�h</param>
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
        /// ���[�J���C�Y�����擾����
        /// </summary>
        /// <param name="key">���[�J���C�Y���̃L�[</param>
        /// <returns>���[�J���C�Y������</returns>
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
        /// ���[�J���C�Y�����擾���A�����t�������񂪐ݒ肳��Ă���ꍇ�͓n���ꂽ�z��ł����u��������
        /// </summary>
        /// <param name="key">���[�J���C�Y���̃L�[</param>
        /// <param name="formatCorrespondingValues">�����t��������ɑΉ�����p�����[�^</param>
        /// <returns></returns>
        public string Format(string key, params string[] formatCorrespondingValues)
        {
            return String.Format(Find(key), formatCorrespondingValues);
        }
    }
}