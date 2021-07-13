using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;

namespace ResourceLoader
{
    /// <summary>
    /// テキストファイルのハッシュ値をSHA256で計算する
    /// </summary>
    public class Sha256FileHashCalcurator: IFileHashCalcurator
    {
        public string Calcurate(TextLoader textLoader)
        {
            var fileContent = textLoader.ReadAll();

            var bytes = Encoding.UTF8.GetBytes(fileContent);
            var cryptoService = new SHA256CryptoServiceProvider();

            var hashedBytes = cryptoService.ComputeHash(bytes);

            StringBuilder hashedText = new StringBuilder();
            for(int byteIndex = 0; byteIndex < hashedBytes.Length; byteIndex++)
            {
                hashedText.AppendFormat("{0:X2}", hashedBytes[byteIndex]);
            }

            return hashedText.ToString();
        }
    }
}