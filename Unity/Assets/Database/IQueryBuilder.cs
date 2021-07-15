using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// データベースに接続するインターフェース
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// 実行するクエリ
        /// </summary>
        public string ExecutionQuery { get; }

        /// <summary>
        /// 条件文(WHERE)
        /// </summary>
        /// <param name="column">カラム名</param>
        /// <param name="comparisonOperator">比較演算子</param>
        /// <param name="value">値</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Where(string column, string comparisonOperator, string value);

        /// <summary>
        /// WHEREにAND条件を組み合わせる
        /// </summary>
        /// <param name="column">カラム名</param>
        /// <param name="comparisonOperator">比較演算子</param>
        /// <param name="value">値</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder AndWhere(string column, string comparisonOperator, string value);

        /// <summary>
        /// WHEREにOR条件を組み合わせる
        /// </summary>
        /// <param name="column">カラム名</param>
        /// <param name="comparisonOperator">比較演算子</param>
        /// <param name="value">値</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder OrWhere(string column, string comparisonOperator, string value);

        /// <summary>
        /// テーブルを指定する
        /// </summary>
        /// <param name="columns"></param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Table(string table);

        /// <summary>
        /// 選択文(SELECT)
        /// </summary>
        /// <param name="columns">カラム名</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Select(params string[] columns);

        /// <summary>
        /// 更新文(UPDATE)
        /// </summary>
        /// <param name="column">カラム名</param>
        /// <param name="value">値</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Update(string column, string value);

        /// <summary>
        /// 挿入文(INSERT)
        /// </summary>
        /// <param name="insertingValues">挿入するカラムと値のペア</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Insert(params string[] insertingValues);


        /// <summary>
        /// 削除文(DELETE)
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IQueryBuilder Delete(string column, string comparisonOperator, string value);

        /*
        /// <summary>
        /// 集計文(GROUP BY)
        /// </summary>
        /// <param name="groupingColumns"></param>
        /// <returns></returns>
        public List<List<Dictionary<string, string>>> ExecuteGroupBy(params string[] groupingColumns);
        */

        /// <summary>
        /// Modelを指定してクエリを実行する
        /// </summary>
        /// <returns>クエリの実行結果</returns>
        public IQueryResult<T> Execute<T>();

        /// <summary>
        /// 単にクエリを実行する
        /// </summary>
        public void Execute();
    }
}