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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reader"></param>
        public ChartAnalyzer(ResourceLoader.TextLoader reader)
        {
            jsonText = reader.ReadAll();
        }

        public Chart Analyze()
        {
            var serializer = new DataContractJsonSerializer(typeof(Chart));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            memoryStream.Seek(0, SeekOrigin.Begin);
            var jsonObject = serializer.ReadObject(memoryStream) as Chart;

            return jsonObject;
        }
    }
}