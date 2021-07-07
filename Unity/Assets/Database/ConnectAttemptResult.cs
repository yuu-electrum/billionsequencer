using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// �ڑ����s����
    /// </summary>
    public class ConnectAttemptResult
    {
        private string errorMessage;
        private AttemptResult attemptResult;

        /// <summary>
        /// ���s���ʂ̎��
        /// </summary>
        public enum AttemptResult
        {
            Succeeded,
            Failed,
            Refused,
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="result">�ڑ����s����</param>
        /// <param name="errorMessage">�G���[���b�Z�[�W</param>
        public ConnectAttemptResult(AttemptResult result, string errorMessage)
        {
            this.errorMessage = errorMessage;
            attemptResult = result;
        }

        /// <summary>
        /// ���s����
        /// </summary>
        public AttemptResult Result
        {
            get
            {
                return attemptResult;
            }
        }

        /// �G���[���b�Z�[�W
        public string ErrorMessage
        {
            get
            {
                return attemptResult == AttemptResult.Succeeded ? "" : errorMessage;
            }
        }
    }
}