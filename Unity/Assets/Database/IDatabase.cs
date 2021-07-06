using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// データベースに接続するインターフェース
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// データベースに接続する
        /// </summary>
        /// <param name="dbConfig">接続情報</param>
        public UniTask<ConnectAttemptResult> Connect(IDatabaseConfiguration dbConfig);

        /// <summary>
        /// クエリを実行する
        /// </summary>
        /// <param name="statement">クエリ文</param>
        /// <returns></returns>
        public UniTask<IQueryResult> Query(string statement);

        /// <summary>
        /// データベースから切断する
        /// </summary>
        public void Disconnect();
    }
}