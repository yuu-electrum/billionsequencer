using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ResourceLoader
{
    /// <summary>
    /// テキストデータを読み込む
    /// </summary>
    public class TextLoader
    {
        private string path;
        private bool fileExists;
       
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">テキストファイルのパス</param>
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
        /// テキストファイルの中身をすべて読み込む
        /// </summary>
        /// <returns>テキストデータ</returns>
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
                // JSONで記述された生のデータを読み込む
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