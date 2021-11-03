using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

namespace ChartSelectScene
{
	/// <summary>
	/// 横方向のリストビュー
	/// </summary>
	public class HorizontalListView: MonoBehaviour
	{
		private Dictionary<int, string> itemKeys;
		private Dictionary<string, int> itemIndexes;
		private Dictionary<string, string> itemLabels;
		private Dictionary<string, Dictionary<int, HorizontalListViewItem>> itemValues;
		private Dictionary<string, RectTransform> listviewRectTransforms;
		private Dictionary<string, OptionItemBase> itemBases;

		private int currentSelectedOptionIndex;
		private Dictionary<string, int> currentSelectedOptionItemIndex;

		[SerializeField]
		private GameObject optionItemBase;

		[SerializeField]
		private TextMeshProUGUI optionDescription;

		[SerializeField]
		private GameObject pivot;

		public const int HorizontalListViewNotFound = -1;

		public void Start()
		{
			currentSelectedOptionIndex = 0;
		}

		/// <summary>
		/// アイテム項目
		/// </summary>
		/// <param name="key">項目のキー</param>
		/// <param name="label">項目に表示される文字列</param>
		public void AddItem(string key, string label)
		{
			if(itemKeys == null)
			{
				// 最初に追加される場合はコレクションを用意する
				itemKeys = new Dictionary<int, string>();
				itemIndexes = new Dictionary<string, int>();
				itemLabels = new Dictionary<string, string>();
				itemValues = new Dictionary<string, Dictionary<int, HorizontalListViewItem>>();
				listviewRectTransforms = new Dictionary<string, RectTransform>();
				itemBases = new Dictionary<string, OptionItemBase>();
				currentSelectedOptionItemIndex = new Dictionary<string, int>();
			}

			var newId = itemKeys.Count;
			itemKeys.Add(newId, key);
			itemIndexes.Add(key, newId);
			itemLabels.Add(key, label);
			itemValues.Add(key, new Dictionary<int, HorizontalListViewItem>());

			var gameObject = Instantiate(optionItemBase, pivot.transform, false);
			listviewRectTransforms.Add(key, gameObject.GetComponent<RectTransform>());
			itemBases.Add(key, gameObject.GetComponent<OptionItemBase>());

			itemBases[key].ItemLabel = label;
		}

		/// <summary>
		/// アイテム項目の値を追加する
		/// </summary>
		/// <param name="key">項目のキー</param>
		/// <param name="itemValue">項目の値</param>
		public void AddItemValue(string key, HorizontalListViewItem itemValue)
		{
			if(!itemIndexes.ContainsKey(key))
			{
				Debug.LogError("Add a key first before adding an item value");
				return;
			}

			itemValues[key].Add(itemValues[key].Count, itemValue);
		}

		/// <summary>
		/// 選択している項目で選択されている値の順番を取得する
		/// </summary>
		/// <param name="key">項目のキー</param>
		/// <param name="value">探す値</param>
		public int FindIndexInSelectionByKey(string key, string value)
		{
			var values = itemValues[key];
			foreach(KeyValuePair<int, HorizontalListViewItem> keyValuePair in values)
			{
				if(keyValuePair.Value.StringValue == value)
				{
					return keyValuePair.Key;
				}
			}

			return HorizontalListViewNotFound;
		}

		/// <summary>
		/// 選択している項目で選択されている値の順番を取得する
		/// </summary>
		/// <param name="key">項目のキー</param>
		/// <param name="value">探す値</param>
		public int FindIndexInSelectionByKey(string key, int value)
		{
			var values = itemValues[key];
			foreach(KeyValuePair<int, HorizontalListViewItem> keyValuePair in values)
			{
				if(keyValuePair.Value.NumberValue == value)
				{
					return keyValuePair.Key;
				}
			}

			return HorizontalListViewNotFound;
		}

		/// <summary>
		/// 次の項目を選択する
		/// </summary>
		public void SelectNext()
		{
			currentSelectedOptionIndex = (currentSelectedOptionIndex + 1) % itemKeys.Count;
			var selectedKey = itemKeys[currentSelectedOptionIndex];
			UpdateSelection(selectedKey);
		}

		/// <summary>
		/// 前の項目を選択する
		/// </summary>
		public void SelectBefore()
		{
			currentSelectedOptionIndex = (currentSelectedOptionIndex + (itemKeys.Count - 1)) % itemKeys.Count;
			var selectedKey = itemKeys[currentSelectedOptionIndex];
			UpdateSelection(selectedKey);
		}

		/// <summary>
		/// インデックスで指定された項目を選択する
		/// </summary>
		/// <param name="key">項目のキー</param>
		/// <param name="index">値の順番</param>
		public void SelectInSelectionByIndex(string key, int index)
		{
			currentSelectedOptionItemIndex[key] = index;
		}

		/// <summary>
		/// 最初の項目を選択する
		/// </summary>
		/// <param name="key">項目のキー</param>
		public void SelectFirstItemInSelection(string key)
		{
			currentSelectedOptionItemIndex[key] = 0;
		}

		/// <summary>
		/// 今選択されている項目の次の値を選択する
		/// </summary>
		/// <param name="key">項目のキー</param>
		public void SelectNextInSelection()
		{
			var key = itemKeys[currentSelectedOptionIndex];
			currentSelectedOptionItemIndex[key] = (currentSelectedOptionItemIndex[key] + 1) % itemValues[key].Values.Count;
			UpdateSelection(key);
		}

		/// <summary>
		/// 今選択されている項目の前の値を選択する
		/// </summary>
		/// <param name="key">項目のキー</param>
		public void SelectBeforeInSelection()
		{
			var key = itemKeys[currentSelectedOptionIndex];
			var count = itemValues[key].Values.Count;
			currentSelectedOptionItemIndex[key] = (currentSelectedOptionItemIndex[key] + (count - 1)) % count;
			UpdateSelection(key);
		}

		/// <summary>
		/// 項目をアップデートする
		/// </summary>
		/// <param name="key">項目のキー</param>
		public void UpdateSelection(string key)
		{
			var selectionIndex = currentSelectedOptionItemIndex[key];
			itemBases[key].ItemValue = itemValues[key][selectionIndex].Label;

			var selectedKey = itemKeys[currentSelectedOptionIndex];
			itemBases[selectedKey].Select();

			if(itemValues.ContainsKey(selectedKey) && itemValues[selectedKey].ContainsKey(selectionIndex))
			{
				optionDescription.text = itemValues[selectedKey][selectionIndex].Description;
			}

			var deselectedItems = itemBases.Where(itemBase => itemBase.Key != selectedKey);
			foreach(var item in deselectedItems)
			{
				item.Value.Deselect();
			}
		}

		/// <summary>
		/// 選択されている選択肢の値を文字列で取得する
		/// </summary>
		public string GetSelectedValueInSelectionAsString(string key)
		{
			var selectionIndex = currentSelectedOptionItemIndex[key];
			return itemValues[key][selectionIndex].StringValue;
		}

		/// <summary>
		/// 選択されている選択肢の値を数値で取得する
		/// </summary>
		public int GetSelectedValueInSelectionAsNumber(string key)
		{
			var selectionIndex = currentSelectedOptionItemIndex[key];
			return itemValues[key][selectionIndex].NumberValue;
		}
	}
}
