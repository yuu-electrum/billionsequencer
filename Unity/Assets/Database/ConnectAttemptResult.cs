using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// 接続試行結果
    /// </summary>
    public class ConnectAttemptResult
    {
        private string errorMessage;
        private AttemptResult attemptResult;

        /// <summary>
        /// 試行結果の種類
        /// </summary>
        public enum AttemptResult
        {
            Succeeded,
            Failed,
            Refused,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="result">接続試行結果</param>
        /// <param name="errorMessage">エラーメッセージ</param>
        public ConnectAttemptResult(AttemptResult result, string errorMessage)
        {
            this.errorMessage = errorMessage;
            attemptResult = result;
        }

        /// <summary>
        /// 試行結果
        /// </summary>
        public AttemptResult Result
        {
            get
            {
                return attemptResult;
            }
        }

        /// エラーメッセージ
        public string ErrorMessage
        {
            get
            {
                return attemptResult == AttemptResult.Succeeded ? "" : errorMessage;
            }
        }
    }
}