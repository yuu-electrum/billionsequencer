using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SQLiteManagement;
using Database.SQLite.Models;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace ChartSelectScene
{
	/// <summary>
	/// 譜面を列挙するクラス
	/// </summary>
	public class ChartListArranger: MonoBehaviour
	{
		[SerializeField]
		private GameObject chartListViewBase;
		private ChartListView listView;
		private bool isEnumerated;

		public async void Start()
		{
			listView = chartListViewBase.AddComponent<ChartListView>();

			isEnumerated = false;
			isEnumerated = await EnumerateCharts(listView);
		}

		public async UniTask<bool> ArrangeListView(ChartListView listView)
		{
			var charts = listView.BoundChartProfileHashes.Values;

			// レベル別フォルダ
			var levelGroups = charts.GroupBy(x => x.Level);

			// スコア別フォルダ

			return true;
		}

		public async UniTask<bool> EnumerateCharts(ChartListView listView)
		{
			var sqlserver = new SQLiteServer();
			sqlserver.Start(Constant.Path.WorkingDirectory, Constant.SQLite.DatabaseInstanceFileName);

			var profiles = sqlserver.InstantiateNewQueryBuilder().Table("chart_profiles").Select("*").Execute<ChartProfile>().Records;
			var hashTable = new Dictionary<string, ChartProfile>();
			foreach(var profile in profiles)
			{
				hashTable.Add(profile.ChartHash, profile);
			}
			listView.BoundChartProfileHashes = hashTable;

			var isArranged = await ArrangeListView(listView);
			await UniTask.WaitUntil(() => isArranged);

			sqlserver.Close();
			return true;
		}
	}
}
