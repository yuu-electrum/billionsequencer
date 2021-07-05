using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.IO;

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

        /// <summary>
        /// ���[�J���C�Y����ǂݍ���
        /// </summary>
        /// <param name="path">���[�J���C�Y��񂪊܂܂��e�L�X�g�t�@�C��</param>
        /// <returns>��������true�A���s����false</returns>
        public bool Load(string path)
        {
            if(path == null || !File.Exists(path))
            {
                Debug.LogFormat("A file {0} is not existing.", path);
                return false;
            }

            try
            {
                // JSON�ŋL�q���ꂽ���̃f�[�^��ǂݍ���
                var streamReader = new StreamReader(path);
                string raw = streamReader.ReadToEnd();
                streamReader.Close();

                // ���[�J���C�Y���Ƃ��ēǂݍ���
                var analyser = new LocalizeAnalyser(raw);
                var localize = analyser.Analyse();

                localizations.Add(localize.Locale, localize);
            }
            catch(Exception e)
            {
                Debug.LogFormat("Failed to load a localization file at {0}. Detail: {1}", path, e.ToString());
            }

            return true;
        }

        /// <summary>
        /// ���P�[����ݒ肷��
        /// </summary>
        /// <param name="locale">���P�[���R�[�h</param>
        public void SetLocale(string locale)
        {
            currentLocale = locale;
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
                Debug.LogFormat("A localization with the current locale {0} is not found.", currentLocale);
                return "";
            }

            return localizations[currentLocale].Find(key);
        }
    }
}