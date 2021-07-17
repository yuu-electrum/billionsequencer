using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constant
{
    /// <summary>
    /// SQLite�Ɋւ���萔
    /// </summary>
    public static class SQLite
    {
        /// <summary>
        /// �f�t�H���g�̃f�[�^�x�[�X�t�@�C����
        /// </summary>
        public static string DatabaseInstanceFileName
        {
            get
            {
                return "game.db";
            }
        }

        /// <summary>
        /// �f�t�H���g�̃v���C���[��
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