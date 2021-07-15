using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    /// <summary>
    /// 階層構造リストのアイテムインターフェース
    /// </summary>
    public interface IHierarchyListItem
    {
        /// <summary>
        /// アイテムの識別子
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// リストで表示されるべき文字列
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 親リスト
        /// </summary>
        public IHierarchyListItem Parent { get; }

        /// <summary>
        /// 子アイテムリスト
        /// </summary>
        public List<IHierarchyListItem> Children { get; }
        
        /// <summary>
        /// 子アイテムを追加する
        /// </summary>
        /// <param name="child">子アイテム</param>
        public void AddChild(IHierarchyListItem child);
    }
}