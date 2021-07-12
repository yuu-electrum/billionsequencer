using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using UnityEngine;
using ResourceLoader;

namespace Localization
{
    /// <summary>
    /// JSONで表現されたローカライズ情報をオブジェクトに変換するクラス
    /// </summary>
    public class LocalizeAnalyzer
    {
        [DataContract]
        public class LocalizeJson
        {
            [DataMember(Name = "language")]
            public string Language { get; set; }

            [DataMember(Name = "localizations")]
            public LocalizePairJson[] Localizations { get; set; }
        }

        [DataContract]
        public class LocalizePairJson
        {
            [DataMember(Name = "key")]
            public string Key { get; set; }

            [DataMember(Name = "replacement")]
            public string Replacement { get; set; }
        }

        private string jsonText;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reader"></param>
        public LocalizeAnalyzer(ResourceLoader.TextLoader reader)
        {
            jsonText = reader.ReadAll();
        }

        /// <summary>
        /// JSONデータを解析する
        /// </summary>
        public Localize Analyze()
        {
            var serializer = new DataContractJsonSerializer(typeof(LocalizeJson));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
            memoryStream.Seek(0, SeekOrigin.Begin);
            var jsonObject = serializer.ReadObject(memoryStream) as LocalizeJson;

            var localize = new Localize(jsonObject.Language);
            foreach(var list in jsonObject.Localizations)
            {
                localize.AddLexicon(list.Key, list.Replacement);
            }

            return localize;
        }
    }
}