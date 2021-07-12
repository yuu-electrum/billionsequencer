using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Constant
{
    /// <summary>
    /// �p�X�Ɋւ���萔
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// �Q�[�������s���Ă���f�B���N�g�����擾����
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
        /// ���ʃt�@�C�����i�[����Ă���f�B���N�g�����擾����
        /// </summary>
        public static string ChartDirectory
        {
            get
            {
                return WorkingDirectory + "\\Charts";
            }
        }
    }
}