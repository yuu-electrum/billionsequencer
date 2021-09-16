using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Database.SQLite.Models;
using ResourceLoader;
using ChartManagement;
using TMPro;

namespace ChartSelectScene
{
	/// <summary>
	/// 譜面リストビュー
	/// </summary>
	public class ChartListView: MonoBehaviour
	{
		private string currentFolderGuid;
		private bool isInFolder;
		private bool isFiltered;
		private int lastSelectedFolderId;
		private int minSelectionId;
		private int maxSelectionId;
		private int currentSelectionId;
		private Dictionary<string, ChartProfile> chartProfileHashes;
		private Dictionary<string, ScoreProfile> chartScoreProfiles;
		private Dictionary<string, Chart> charts;
		private Dictionary<string, ChartListViewFolder> folderGuidTable;
		private Dictionary<int, string> folderGuidIndexTable;

		[SerializeField]
		private GameObject listViewItemBaseObject;

		[SerializeField]
		private RectTransform listViewItemBaseRectTransform;

		[SerializeField]
		private RawImage artwork;

		[SerializeField]
		private TextMeshProUGUI score;

		[SerializeField]
		private TextMeshProUGUI playResult;

		[SerializeField]
		private TextMeshProUGUI artist;

		[SerializeField]
		private TextMeshProUGUI sequenceDesigner;

		[SerializeField]
		private int maximumListViewItemCount = 34;

		[SerializeField]
		private Animator listViewItemFadeinAnimator;

		[SerializeField]
		private Animator[] animators;

		private List<GameObject> listObjects;
		private Dictionary<int, ListViewItem> listViewItems;
		private Dictionary<int, RectTransform> rectTransforms;

		private RectTransform selfRectTransform;

		/// <summary>
		/// フォルダのカーソルの移動方向
		/// </summary>
		public enum MoveSelectionDirection
		{
			Before,
			Next
		}

		public void Awake()
		{
			currentFolderGuid = null;
			isInFolder = false;
			chartProfileHashes = new Dictionary<string, ChartProfile>();
			charts = new Dictionary<string, Chart>();
			folderGuidTable = new Dictionary<string, ChartListViewFolder>();
			folderGuidIndexTable = new Dictionary<int, string>();

			listObjects = new List<GameObject>();
			listViewItems = new Dictionary<int, ListViewItem>();
			rectTransforms = new Dictionary<int, RectTransform>();
			selfRectTransform = GetComponent<RectTransform>();
			isFiltered = false;
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
		/// リストビューに紐づいたスコアデータのハッシュテーブル
		/// </summary>
		public Dictionary<string, ScoreProfile> BoundScoreProfileHashes
		{
			get
			{
				return chartScoreProfiles;
			}

			set
			{
				chartScoreProfiles = value;
			}
		}

		/// <summary>
		/// レイアウト
		/// </summary>
		public IEnumerator Layout(int preselectSelectionId = 0)
		{
			var baseObjectHeight = listViewItemBaseRectTransform.rect.height;
			
			if(!isInFolder)
			{
				var currentItemIndex = 0;

				foreach(var rectTrans in rectTransforms)
				{
					Destroy(rectTrans.Value.gameObject);
				}
				rectTransforms.Clear();
				listViewItems.Clear();

				foreach(KeyValuePair<string, ChartListViewFolder> folder in folderGuidTable)
				{
					// フォルダに入っていない場合、単にフォルダを列挙する
					var newGameObject = Instantiate(listViewItemBaseObject);
					newGameObject.transform.SetParent(this.transform, false);
					var rectTransform = newGameObject.GetComponent<RectTransform>();
					rectTransforms.Add(newGameObject.GetInstanceID(), rectTransform);

					var currentPosition = rectTransform.anchoredPosition;
					currentPosition.y = (currentItemIndex * -1) * baseObjectHeight;
					rectTransform.anchoredPosition = currentPosition;

					var textMeshPro = newGameObject.GetComponent<TextMeshProUGUI>();
					textMeshPro.text = folder.Value.Label;

					currentItemIndex++;
					yield return null;
				}

				// 選択できる範囲を設定する
				minSelectionId = 0;
				maxSelectionId = folderGuidTable.Count - 1;
				currentSelectionId = minSelectionId;

				score.gameObject.SetActive(false);
				playResult.gameObject.SetActive(false);
				artist.gameObject.SetActive(false);
				sequenceDesigner.gameObject.SetActive(false);
				MoveSelection(MoveSelectionDirection.Next, preselectSelectionId);
			}
			else if(folderGuidTable.ContainsKey(currentFolderGuid))
			{
				var currentItemIndex = 0;
				var folder = folderGuidTable[currentFolderGuid].Charts;

				foreach(var rectTrans in rectTransforms)
				{
					Destroy(rectTrans.Value.gameObject);
				}
				rectTransforms.Clear();
				listViewItems.Clear();

				foreach(var chart in folder)
				{
					// フォルダに入っている場合、楽曲を列挙する
					var newGameObject = Instantiate(listViewItemBaseObject);
					newGameObject.transform.SetParent(this.transform, false);
					var rectTransform = newGameObject.GetComponent<RectTransform>();
					rectTransforms.Add(newGameObject.GetInstanceID(), rectTransform);

					var currentPosition = rectTransform.anchoredPosition;
					currentPosition.y = (currentItemIndex * -1) * baseObjectHeight;
					rectTransform.anchoredPosition = currentPosition;

					var textMeshPro = newGameObject.GetComponent<TextMeshProUGUI>();
					textMeshPro.text = BoundChartProfileHashes[chart].Title;

					currentItemIndex++;
					yield return null;
				}

				// 選択できる範囲を設定する
				minSelectionId = 0;
				maxSelectionId = folder.Count - 1;
				currentSelectionId = minSelectionId;

				StartCoroutine(ShowChartDetails());
			}

			foreach(var rectTransform in rectTransforms)
			{
				// 画面に表示されない項目は非表示にする
				var positionY = rectTransform.Value.anchoredPosition.y;

				var visibleBoundary = selfRectTransform.rect.height / 2.0f;
				var isVisible = positionY < visibleBoundary && positionY > visibleBoundary * -1;

				var gameObjectInstanceId = rectTransform.Value.gameObject.GetInstanceID();
				listViewItems.Add(gameObjectInstanceId, rectTransform.Value.gameObject.GetComponent<ListViewItem>());
				
				if(!isVisible)
				{
					listViewItems[gameObjectInstanceId].FinishAnimator();
					rectTransform.Value.gameObject.SetActive(false);
				}
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
		/// <param name="guid">GUID</param>
		/// <returns></returns>
		public ChartListViewFolder GetFolder(string guid)
		{
			if(!folderGuidTable.ContainsKey(guid))
			{
				return null;
			}

			return folderGuidTable.ContainsKey(guid) ? folderGuidTable[guid] : null;
		}

		/// <summary>
		/// 選択しているカーソルを移動する
		/// </summary>
		/// <param name="moveDirection">移動方向</param>
		/// <param name="moveCount">移動量</param>
		/// <returns></returns>
		public void MoveSelection(MoveSelectionDirection moveDirection, int moveCount)
		{
			var direction = moveDirection == MoveSelectionDirection.Next ? 1 : -1;
			var nextSelection = currentSelectionId + moveCount * direction;

			if(nextSelection > maxSelectionId || nextSelection < 0 || rectTransforms.Count == 0)
			{
				return;
			}
			currentSelectionId = nextSelection;

			if(isInFolder)
			{
				StartCoroutine(ShowChartDetails());
			}

			var firstSelection = rectTransforms.First();

			var height = firstSelection.Value.rect.height;
			var positionY = currentSelectionId * height;
			foreach(var rectTransform in rectTransforms)
			{
				var position = rectTransform.Value.anchoredPosition;
				position.y = positionY;

				var visibleBoundary = selfRectTransform.rect.height / 2.0f;
				var isVisible = positionY < visibleBoundary && positionY > visibleBoundary * -1;

				if(listViewItems.ContainsKey(rectTransform.Value.gameObject.GetInstanceID()))
				{
					listViewItems[rectTransform.Value.gameObject.GetInstanceID()].FinishAnimator();
				}
				rectTransform.Value.gameObject.SetActive(isVisible);

				rectTransform.Value.anchoredPosition = position;

				positionY -= height;
			}
		}

		/// <summary>
		/// 選択しているカーソルを移動する
		/// </summary>
		public void ExecuteSelection()
		{
			if(currentSelectionId >= folderGuidTable.Count && currentSelectionId < 0)
			{
				return;
			}

			if(!isInFolder)
			{
				var selectedFolderGuid = folderGuidIndexTable[currentSelectionId];

				isInFolder = true;
				currentFolderGuid = selectedFolderGuid;
				lastSelectedFolderId = currentSelectionId;

				StartCoroutine(Layout());
			}
			else
			{
				// 曲を確定する
			}
		}

		/// <summary>
		/// フォルダリストを表示する
		/// </summary>
		public void ShowFolderList()
		{
			if(!isInFolder)
			{
				return;
			}

			isInFolder = false;
			currentFolderGuid = "";

			StartCoroutine(Layout(lastSelectedFolderId));
			artwork.gameObject.SetActive(false);
		}

		/// <summary>
		/// ハッシュ値に紐づくアートワークを読み込む
		/// </summary>
		public IEnumerator LoadArtwork(IDynamicImageLoader imageLoader, string hash)
		{
			var chart = BoundChartProfileHashes[hash];
			var chartDirectory = Path.GetDirectoryName(chart.FilePath);

			Chart cursorChart = null;

			if(charts.ContainsKey(hash))
			{
				cursorChart = charts[hash];
			}
			else
			{
				// 譜面情報をキャッシュしておく
				var newAnalyzer = new ChartAnalyzer(new Sha256FileHashCalcurator(), new TextLoader(chart.FilePath));
				charts.Add(hash, newAnalyzer.Analyze());
				cursorChart = charts[hash];	
			}

			var artworkFileName = cursorChart.GlobalConfigurations.Artwork;
			var artworkFilePath = chartDirectory + "/" + artworkFileName;

			if(!File.Exists(artworkFilePath))
			{
				artwork.gameObject.SetActive(false);
				yield break;
			}

			StartCoroutine(imageLoader.Load(artworkFilePath));

			yield return new WaitUntil(() => imageLoader.IsLoaded);

			artwork.gameObject.SetActive(true);
			artwork.texture = imageLoader.Image;

			yield break;
		}

		/// <summary>
		/// 譜面の情報を表示する
		/// </summary>
		private IEnumerator ShowChartDetails()
		{
			StartCoroutine(LoadArtwork(new PngImageLoader(), folderGuidTable[currentFolderGuid].GetChartHash(currentSelectionId)));
				
			var hash = folderGuidTable[currentFolderGuid].GetChartHash(currentSelectionId);

			// アートワークを読み込む
			StartCoroutine(LoadArtwork(new PngImageLoader(), hash));
				
			// Animatorを強制的に再実行させる
			score.gameObject.SetActive(true);
			playResult.gameObject.SetActive(true);
			artist.gameObject.SetActive(true);
			sequenceDesigner.gameObject.SetActive(true);

			foreach(var animator in animators)
			{
				animator.Rebind();
				animator.Update(0.0f);
			}

			// 譜面の情報を表示する
			score.text = Localization.LocalizeLoader.Instance.Format("Score", chartScoreProfiles[hash].Score.ToString());
			var chartPlayResult = Localization.LocalizeLoader.Instance.Find("PlayResultNeverPlayed");
			switch(chartScoreProfiles[hash].PlayResult)
			{
				case "failed":
					chartPlayResult = Localization.LocalizeLoader.Instance.Find("PlayResultFailed");
					break;

				case "succeeded_over_reference":
					chartPlayResult = Localization.LocalizeLoader.Instance.Find("PlayResultOverReference");
					break;

				case "succeeded_life_retaining":
					chartPlayResult = Localization.LocalizeLoader.Instance.Find("PlayResultLifeRetaining");
					break;
			}
			playResult.text = chartPlayResult;
			artist.text = chartProfileHashes[hash].Artist;
			sequenceDesigner.text = Localization.LocalizeLoader.Instance.Format("SequenceDesigner", chartProfileHashes[hash].SequenceDesigner);
			yield break;
		}
	}
}