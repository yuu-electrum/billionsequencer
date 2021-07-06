using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SQLiteServer
{
    namespace Migrations
    {
        /// <summary>
        /// マイグレーションのインターフェース
        /// </summary>
        public interface IMigration
        {
            /// <summary>
            /// マイグレーションID。小さいほど優先的に実行される
            /// </summary>
            /// <returns></returns>
            public int Id { get; }

            /// <summary>
            /// マイグレーションを実行
            /// </summary>
            public string ExecutionQuery { get; }
        }
    }
}