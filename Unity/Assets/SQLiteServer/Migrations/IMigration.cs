using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SQLiteServer
{
    namespace Migrations
    {
        /// <summary>
        /// �}�C�O���[�V�����̃C���^�[�t�F�[�X
        /// </summary>
        public interface IMigration
        {
            /// <summary>
            /// �}�C�O���[�V����ID�B�������قǗD��I�Ɏ��s�����
            /// </summary>
            /// <returns></returns>
            public int Id { get; }

            /// <summary>
            /// �}�C�O���[�V���������s
            /// </summary>
            public string ExecutionQuery { get; }
        }
    }
}