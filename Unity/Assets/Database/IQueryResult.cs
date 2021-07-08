using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    /// <summary>
    /// ƒNƒGƒŠŒ‹‰Ê
    /// </summary>
    public interface IQueryResult<T>
    {
        public List<T> Records { get; }
        public int RecordCount { get; }
        public string ExecutedQuery { get; }
        public string ErrorMessage { get; }
    }
}