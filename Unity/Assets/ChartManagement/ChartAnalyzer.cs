using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using UnityEngine;
using ResourceLoader;

namespace ChartManagement
{
    /// <summary>
    /// 譜面を読み込んで解析するクラス
    /// </summary>
    public class ChartAnalyzer
    {
        private string jsonText;
        private string hash;
        private string filePath;
        private IFileHashCalcurator fileHashCalcurator;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reader"></param>
        public ChartAnalyzer(IFileHashCalcurator fileHashCalcurator, ResourceLoader.TextLoader reader)
        {
            jsonText = reader.ReadAll();
            this.fileHashCalcurator = fileHashCalcurator;
            hash = this.fileHashCalcurator.Calcurate(reader);
            filePath = reader.Path;
        }

        /// <summary>
        /// 譜面を解析する
        /// </summary>
        public Chart Analyze()
        {
            var serializer = new DataContractJsonSerializer(typeof(Chart));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            memoryStream.Seek(0, SeekOrigin.Begin);
            var jsonObject = serializer.ReadObject(memoryStream) as Chart;

            return jsonObject;
        }

        /// <summary>
        /// 譜面のハッシュ値を取得する
        /// </summary>
        public string Hash
        {
            get
            {
                return hash;
            }
        }

        /// <summary>
        /// 譜面のパスを取得する
        /// </summary>
        public string Path
        {
            get
            {
                return filePath;
            }
        }
    }
}