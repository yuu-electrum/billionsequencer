using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database.SQLite.Models;

namespace ChartSelectScene
{
	/// <summary>
	/// 譜面リストビュー
	/// </summary>
	public class ChartListView: MonoBehaviour
	{
		private Dictionary<string, ChartProfile> chartProfileHashes;
		private Dictionary<string, ChartListViewFolder> folderGuidTable;
		private Dictionary<int, string> folderGuidIndexTable;

		public void Awake()
		{
			chartProfileHashes = new Dictionary<string, ChartProfile>();
			folderGuidTable = new Dictionary<string, ChartListViewFolder>();
			folderGuidIndexTable = new Dictionary<int, string>();
		}

		/// <summary>
		/// リストビューに紐づいた譜面のハッシュテーブル
		/// </summary>
		public Dictionary<string, ChartProfile> BoundChartProfileHashes
		{
			get
			{
				return chartProfileHashes;
			}

			set
			{
				chartProfileHashes = value;
			}
		}

		/// <summary>
		/// フォルダを追加する
		/// </summary>
		/// <param name="folder"></param>
		public void AddFolder(ChartListViewFolder folder)
		{
			// フォルダ番号とGUID両方からフォルダを一意に特定できるようにする
			folderGuidIndexTable.Add(folderGuidIndexTable.Count, folder.Guid);
			folderGuidTable.Add(folder.Guid, folder);
		}

		/// <summary>
		/// フォルダ番号からフォルダを特定する
		/// </summary>
		/// <param name="index">フォルダ番号</param>
		/// <returns></returns>
		public ChartListViewFolder GetFolder(int index)
		{
			if(!folderGuidIndexTable.ContainsKey(index))
			{
				return null;
			}

			return folderGuidTable.ContainsKey(folderGuidIndexTable[index]) ? folderGuidTable[folderGuidIndexTable[index]] : null;
		}

		/// <summary>
		/// GUIDからフォルダを特定する
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public ChartListViewFolder GetFolder(string guid)
		{
			if(!folderGuidTable.ContainsKey(guid))
			{
				return null;
			}

			return folderGuidTable.ContainsKey(guid) ? folderGuidTable[guid] : null;
		}
	}
}