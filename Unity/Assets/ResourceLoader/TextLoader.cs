using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ResourceLoader
{
    /// <summary>
    /// �e�L�X�g�f�[�^��ǂݍ���
    /// </summary>
    public class TextLoader
    {
        private string path;
        private bool fileExists;
       
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="path">�e�L�X�g�t�@�C���̃p�X</param>
        public TextLoader(string path)
        {
            this.path = path;
            if(path == null || !File.Exists(path))
            {
                Debug.LogErrorFormat("A text file {0} is not existing.", path);
                fileExists = false;
            }
            else
            {
                fileExists = true;
            }
        }

        /// <summary>
        /// �e�L�X�g�t�@�C���̒��g�����ׂēǂݍ���
        /// </summary>
        /// <returns>�e�L�X�g�f�[�^</returns>
        public string ReadAll()
        {
            var streamReader = new StreamReader(path);
            var content = "";

            if(!fileExists)
            {
                return "";
            }

            try
            {
                // JSON�ŋL�q���ꂽ���̃f�[�^��ǂݍ���
                content = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch(Exception e)
            {
                Debug.LogErrorFormat("Failed to load a localization file at {0}. Detail: {1}", path, e.ToString());
            }

            return content;
        }
    }
}