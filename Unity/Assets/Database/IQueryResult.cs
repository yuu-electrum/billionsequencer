using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// �N�G������
    /// </summary>
    public interface IQueryResult
    {
        public int RecordCount { get; }
        public string ExecutedQuery { get; }
    }
}