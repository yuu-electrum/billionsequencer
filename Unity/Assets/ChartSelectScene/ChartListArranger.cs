using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
		private ChartListView listView;

		private bool isEnumerated;
		private bool isCurrentSceneTransitionComplete;
		private bool isListFirstLayouted;

		public async void Start()
		{
			isEnumerated = false;
			isCurrentSceneTransitionComplete = false;
			isListFirstLayouted = false;
			isEnumerated = await EnumerateCharts(listView);
		}

		public void Update()
		{
			if(SceneManager.sceneCount == 1 && SceneManager.GetSceneAt(0).name == "ChartSelectScene")
			{
				// 選曲画面への遷移が完全に終わっているか
				isCurrentSceneTransitionComplete = true;
			}

			if(isCurrentSceneTransitionComplete && !isListFirstLayouted)
			{
				StartCoroutine(listView.Layout());
				isListFirstLayouted = true;
			}
		}

		/// <summary>
		/// ドラム式リストビューを初期化する
		/// </summary>
		/// <param name="listView">ドラム式リストビュー</param>
		/// <returns></returns>
		public async UniTask<bool> ArrangeListView(ChartListView listView)
		{
			var charts = listView.BoundChartProfileHashes.Values;

			// レベル別フォルダ
			var levelGroups = charts.GroupBy(x => x.Level);
			foreach(var levelGroup in levelGroups)
            {
				var label = Localization.LocalizeLoader.Instance.Format("Level", levelGroup.Key.ToString());
				var folder = new ChartListViewFolder(label, listView);
				foreach(var chart in levelGroup)
                {
					folder.AddChart(chart.ChartHash);
                }

				listView.AddFolder(folder);
            }

			return true;
		}

		/// <summary>
		/// 譜面を列挙する
		/// </summary>
		/// <param name="listView">ドラム式リストビュー</param>
		/// <returns></returns>
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
