using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartSelectScene
{
	/// <summary>
	/// 読み込んだ譜面リストビューのフォルダ
	/// </summary>
	public class ChartListViewFolder
	{
		private string guid;
		private string label;
		private List<string> chartHashes;
		private ChartListView view;

		/// <summary>
		/// コンスタント
		/// </summary>
		/// <param name="label">フォルダのラベル</param>
		/// <param name="boundProfiles"></param>
		public ChartListViewFolder(string label, ChartListView view)
		{
			chartHashes = new List<string>();

			guid = System.Guid.NewGuid().ToString();
			this.label = label;
			this.view = view;
		}

		/// <summary>
		/// フォルダの一時的なGUID
		/// </summary>
		public string Guid
		{
			get
			{
				return guid;
			}
		}

		/// <summary>
		/// フォルダのタイトル
		/// </summary>
		public string Label
		{
			get
			{
				return label;
			}
		}

		/// <summary>
		/// フォルダに紐づく譜面のハッシュ値リスト
		/// </summary>
		public List<string> Charts
		{
			get
			{
				return chartHashes;
			}
		}

		/// <summary>
		/// 譜面をフォルダに追加する
		/// </summary>
		/// <param name="hash">譜面のハッシュ値</param>
		public void AddChart(string hash)
		{
			chartHashes.Add(hash);
		}
	}
}