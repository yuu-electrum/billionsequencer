using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChartSelectScene
{
	/// <summary>
	/// 横方向のリストビュー
	/// </summary>
	public class HorizontalListView: MonoBehaviour
	{
		private Dictionary<int, string> itemIndexes;
		private Dictionary<string, string> itemValues;
		private Dictionary<string, string> itemLabels;

		private int currentSelectedIndex;

		[SerializeField]
		private GameObject optionItemBase;

		[SerializeField]
		private GameObject pivot;

		public void Start()
		{
			currentSelectedIndex = 0;
		}

		/// <summary>
		/// リストビューにアイテムを追加する
		/// </summary>
		public void AddItem(string key, string value, string label)
		{
			if(itemIndexes == null)
			{
				// 最初に追加される場合はコレクションを用意する
				itemIndexes = new Dictionary<int, string>();
				itemValues = new Dictionary<string, string>();
				itemLabels = new Dictionary<string, string>();
			}

			var newId = itemIndexes.Count;
			itemIndexes.Add(newId, key);
			itemValues.Add(key, value);
			itemLabels.Add(key, label);

			Instantiate(optionItemBase, pivot.transform, false);
		}

		/// <summary>
		/// 次の項目を選択する
		/// </summary>
		public void SelectNext()
		{
			currentSelectedIndex = (currentSelectedIndex + 1) % itemIndexes.Count;
		}

		/// <summary>
		/// 前の項目を選択する
		/// </summary>
		public void SelectBefore()
		{
			currentSelectedIndex = (currentSelectedIndex + (itemIndexes.Count - 1)) % itemIndexes.Count;
		}
	}
}
