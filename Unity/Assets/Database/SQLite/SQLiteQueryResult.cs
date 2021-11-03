using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLiteUnity;

namespace Database.SQLite
{
    /// <summary>
    /// SQLiteのクエリ結果
    /// </summary>
    public class SQLiteQueryResult<T>: IQueryResult<T>
    {
        private List<T> models;
        private int recordCount;
        private string executionQuery;
        private string errorMessage;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="queryResultTable">クエリ結果</param>
        public SQLiteQueryResult(SQLiteTable queryResultTable, string executionQuery, string errorMessage = null)
        {
            models = new List<T>();
            recordCount = queryResultTable.Rows.Count;
            this.errorMessage = errorMessage;
            this.executionQuery = executionQuery;

            foreach(var row in queryResultTable.Rows)
            {
                models.Add((T)Activator.CreateInstance(typeof(T), new object[] {row}));
            }
        }

        public List<T> Records
        {
            get
            {
                return models;
            }
        }

        public int RecordCount
        {
            get
            {
                return recordCount;
            }
        }

        public string ExecutedQuery
        {
            get
            {
                return executionQuery;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return string.IsNullOrEmpty(errorMessage) ? "" : errorMessage;
            }
        }
    }
}