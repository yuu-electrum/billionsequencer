using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ResourceLoader
{
    /// <summary>
    /// �w�肳�ꂽ�t�@�C���̃n�b�V���l���v�Z����C���^�[�t�F�[�X
    /// </summary>
    public interface IFileHashCalcurator
    {
        /// <summary>
        /// �t�@�C����ǂݍ��݁A�n�b�V���l���v�Z����
        /// </summary>
        /// <returns></returns>
        public string Calcurate(TextLoader textLoader);
    }
}