using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Database
{
    /// <summary>
    /// �f�[�^�x�[�X�ɐڑ�����C���^�[�t�F�[�X
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// �f�[�^�x�[�X�ɐڑ�����
        /// </summary>
        /// <param name="dbConfig">�ڑ����</param>
        public UniTask<ConnectAttemptResult> Connect(IDatabaseConfiguration dbConfig);

        /// <summary>
        /// �N�G�������s����
        /// </summary>
        /// <param name="statement">�N�G����</param>
        /// <returns></returns>
        public UniTask<IQueryResult> Query(string statement);

        /// <summary>
        /// �f�[�^�x�[�X����ؒf����
        /// </summary>
        public void Disconnect();
    }
}