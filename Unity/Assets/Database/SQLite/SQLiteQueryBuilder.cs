using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

namespace Database.SQLite
{
    /// <summary>
    /// SQLiteのクエリビルダ
    /// </summary>
    public class SQLiteQueryBuilder: IQueryBuilder
    {
        private SQLiteUnity.SQLite sqLiteInstance;
        private StringBuilder stringParts;
        private string table;

        public string ExecutionQuery
        {
            get
            {
                return string.Format("{0};", stringParts.ToString());
            }
        }

        public SQLiteQueryBuilder(SQLiteUnity.SQLite sqLiteInstance)
        {
            this.sqLiteInstance = sqLiteInstance;
            stringParts = new StringBuilder();
        }

        public IQueryBuilder Where(string column, string comparisonOperator, string value)
        {
            stringParts.Append(string.Format("WHERE {0} {1} {2}{3}", column, comparisonOperator, QuotationConverter.Convert(value), System.Environment.NewLine));
            return this;
        }

        public IQueryBuilder AndWhere(string column, string comparisonOperator, string value)
        {
            stringParts.Append(string.Format("AND {0} {1} {2}{3}", column, comparisonOperator, QuotationConverter.Convert(value), System.Environment.NewLine));
            return this;
        }

        public IQueryBuilder OrWhere(string column, string comparisonOperator, string value)
        {
            stringParts.Append(string.Format("OR {0} {1} {2}{3}", column, comparisonOperator, QuotationConverter.Convert(value), System.Environment.NewLine));
            return this;
        }

        public IQueryBuilder Table(string table)
        {
            this.table = table;
            return this;
        }

        public IQueryBuilder Select(params string[] columns)
        {
            if(string.IsNullOrEmpty(table) || columns.Length <= 0)
            {
                return this;
            }

            stringParts.Append("SELECT ");
            for(var columnIndex = 0; columnIndex < columns.Length; columnIndex++)
            {
                if(columnIndex == columns.Length - 1)
                {
                    stringParts.Append(string.Format("{0} ", columns[columnIndex]));
                }
                else
                {
                    stringParts.Append(string.Format("{0}, ", columns[columnIndex]));
                }
            }

            stringParts.Append(string.Format("{0}FROM {1}{2}", System.Environment.NewLine, table, System.Environment.NewLine));
            return this;
        }

        public IQueryBuilder Update(string column, string value)
        {
            if(string.IsNullOrEmpty(table))
            {
                return this;
            }

            stringParts.Append(string.Format("UPDATE {0}{1}SET {2} = {3}{4}", table, System.Environment.NewLine, column, QuotationConverter.Convert(value), System.Environment.NewLine));
            return this;
        }

        public IQueryBuilder Insert(params string[] insertingValues)
        {
            if(string.IsNullOrEmpty(table))
            {
                return this;
            }

            stringParts.Append(string.Format("INSERT INTO {0} VALUES (", table));
            for(var columnIndex = 0; columnIndex < insertingValues.Length; columnIndex++)
            {
                var insertingValue = QuotationConverter.Convert(insertingValues[columnIndex]);

                if(columnIndex == insertingValues.Length - 1)
                {
                    stringParts.Append(string.Format("{0}){1}", insertingValue, System.Environment.NewLine));
                }
                else
                {
                    stringParts.Append(string.Format("{0}, ", insertingValue));
                }
            }

            return this;
        }

        public IQueryBuilder Delete(string column, string comparisonOperator, string value)
        {
            if(string.IsNullOrEmpty(table))
            {
                return this;
            }

            stringParts.Append(string.Format("DELETE FROM {0} WHERE {1} {2} {3}", table, column, comparisonOperator, QuotationConverter.Convert(value)));

            return this;
        }

        /*
        public IQueryBuilder ExecuteGroupBy(params string[] groupingColumns)
        {
            if(string.IsNullOrEmpty(table))
            {
                return this;
            }

            stringParts.Append(string.Format("GROUP BY "));
            for(var columnIndex = 0; columnIndex < groupingColumns.Length; columnIndex++)
            {
                if(columnIndex < groupingColumns.Length - 1)
                {
                    stringParts.Append(string.Format("{0}, ", groupingColumns[columnIndex]));
                }
                else
                {
                    stringParts.Append(string.Format("{0}{1};", groupingColumns[columnIndex], System.Environment.NewLine));
                }
            }

            return this;
        }
        */

        public IQueryResult<T> Execute<T>()
        {
            var result = sqLiteInstance.ExecuteQuery(ExecutionQuery);

            return new SQLiteQueryResult<T>(result, ExecutionQuery);
        }

        public void Execute()
        {
            sqLiteInstance.ExecuteQuery(ExecutionQuery);
        }
    }
}