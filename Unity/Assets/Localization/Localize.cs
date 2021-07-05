using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    /// <summary>
    /// ���[�J���C�Y���N���X
    /// </summary>
    public class Localize
    {
        private string locale;
        private Dictionary<string, string> lexicon;

        /// <summary>
        /// ���P�[�������擾����
        /// </summary>
        public string Locale
        {
            get
            {
                return locale;
            }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="locale"></param>
        public Localize(string locale)
        {
            this.locale = locale;

            lexicon = new Dictionary<string, string>();
        }

        /// <summary>
        /// ���[�J���C�Y����ǉ�����
        /// </summary>
        /// <param name="key">�擾����ۂ̃L�[</param>
        /// <param name="replacement">�u�������镶����</param>
        public void AddLexicon(string key, string replacement)
        {
            lexicon.Add(key, replacement);
        }

        /// <summary>
        /// ���[�J���C�Y����T��
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