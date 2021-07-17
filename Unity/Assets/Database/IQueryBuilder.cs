using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// �f�[�^�x�[�X�ɐڑ�����C���^�[�t�F�[�X
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// ���s����N�G��
        /// </summary>
        public string ExecutionQuery { get; }

        /// <summary>
        /// ������(WHERE)
        /// </summary>
        /// <param name="column">�J������</param>
        /// <param name="comparisonOperator">��r���Z�q</param>
        /// <param name="value">�l</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Where(string column, string comparisonOperator, string value);

        /// <summary>
        /// WHERE��AND������g�ݍ��킹��
        /// </summary>
        /// <param name="column">�J������</param>
        /// <param name="comparisonOperator">��r���Z�q</param>
        /// <param name="value">�l</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder AndWhere(string column, string comparisonOperator, string value);

        /// <summary>
        /// WHERE��OR������g�ݍ��킹��
        /// </summary>
        /// <param name="column">�J������</param>
        /// <param name="comparisonOperator">��r���Z�q</param>
        /// <param name="value">�l</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder OrWhere(string column, string comparisonOperator, string value);

        /// <summary>
        /// �e�[�u�����w�肷��
        /// </summary>
        /// <param name="columns"></param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Table(string table);

        /// <summary>
        /// �I��(SELECT)
        /// </summary>
        /// <param name="columns">�J������</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Select(params string[] columns);

        /// <summary>
        /// �X�V��(UPDATE)
        /// </summary>
        /// <param name="column">�J������</param>
        /// <param name="value">�l</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Update(string column, string value);

        /// <summary>
        /// �}����(INSERT)
        /// </summary>
        /// <param name="insertingValues">�}������J�����ƒl�̃y�A</param>
        /// <returns>IQueryBuilder</returns>
        public IQueryBuilder Insert(params string[] insertingValues);


        /// <summary>
        /// �폜��(DELETE)
        /// </summary>
        /// <param name="column"></param>
        /// <param name="comparisonOperator"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IQueryBuilder Delete(string column, string comparisonOperator, string value);

        /*
        /// <summary>
        /// �W�v��(GROUP BY)
        /// </summary>
        /// <param name="groupingColumns"></param>
        /// <returns></returns>
        public List<List<Dictionary<string, string>>> ExecuteGroupBy(params string[] groupingColumns);
        */

        /// <summary>
        /// Model���w�肵�ăN�G�������s����
        /// </summary>
        /// <returns>�N�G���̎��s����</returns>
        public IQueryResult<T> Execute<T>();

        /// <summary>
        /// �P�ɃN�G�������s����
        /// </summary>
        public void Execute();
    }
}