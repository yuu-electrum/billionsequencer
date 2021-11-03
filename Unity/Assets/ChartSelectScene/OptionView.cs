using System.Collections;
using System.Collections.Generic;
using ConfigurationManagement;
using Localization;
using UnityEngine;
using TMPro;

namespace ChartSelectScene
{
	/// <summary>
	/// オプションビュー
	/// </summary>
	public class OptionView: MonoBehaviour
	{
		/// <summary>
		/// クリア基準
		/// </summary>
		public enum GameSuccessReference
		{
			/// <summary>
			/// 増加型ゲージ
			/// </summary>
			OverReference,

			/// <summary>
			/// 減少型ゲージ
			/// </summary>
			LifeRetaining
		}

		[SerializeField]
		private GameObject mask;

		[SerializeField]
		private HorizontalListView horizontalListView;

		private int scrollSpeedPercentage;
		private GameSuccessReference successReference;

		private ConfigurationManager<PlayerConfiguration> configuration;
		private PlayerConfiguration playerConfiguration;

		/// <summary>
		/// 譜面のスクロールスピード
		/// </summary>
		public int ScrollSpeed
		{
			get
			{
				return scrollSpeedPercentage;
			}
		}

		/// <summary>
		/// クリア基準
		/// </summary>
		public GameSuccessReference SuccessReference
		{
			get
			{
				return successReference;
			}
		}

		public void Start()
		{
			// 設定ファイルが存在しない場合のデフォルトの設定を作成する
			var defaultConfiguration = new PlayerConfiguration();
			defaultConfiguration.clearReference = "over_reference";
			defaultConfiguration.scrollSpeed = 100;
			
			configuration = new ConfigurationManager<PlayerConfiguration>
			(
				Constant.Path.WorkingDirectory + Constant.Path.ConfigurationFilePath,
				defaultConfiguration
			);
			playerConfiguration = configuration.Load();

			// 設定項目を追加する
			horizontalListView.AddItem("clearReference", LocalizeLoader.Instance.Find("ClearReference"));
			horizontalListView.AddItem
			(
				"scrollSpeed",
				LocalizeLoader.Instance.Format
				(
					"ScrollSpeed",
					(Constant.Play.SlowestScrollSpeed / 100.0).ToString(),
					(Constant.Play.FastestScrollSpeed / 100.0).ToString(),
					LocalizeLoader.Instance.Find("ScrollSpeedMultiplier")
				)
			);

			// クリア基準
			horizontalListView.AddItemValue
			(
				"clearReference",
				new HorizontalListViewItem
				(
					LocalizeLoader.Instance.Find("PlayResultOverReference"),
					"over_reference",
					LocalizeLoader.Instance.Find("ClearReferencePlayResultOverReferenceDescription")
				)
			);
			horizontalListView.AddItemValue
			(
				"clearReference",
				new HorizontalListViewItem
				(
					LocalizeLoader.Instance.Find("PlayResultLifeRetaining"),
					"life_retaining",
					LocalizeLoader.Instance.Find("ClearReferencePlayResultLifeRetainingDescription")
				)
			);

			horizontalListView.SelectInSelectionByIndex
			(
				"clearReference",
				horizontalListView.FindIndexInSelectionByKey("clearReference", playerConfiguration.clearReference)
			);
			horizontalListView.UpdateSelection("clearReference");

			// スクロールスピード
			var multiplier = LocalizeLoader.Instance.Find("ScrollSpeedMultiplier");
			for(var i = Constant.Play.SlowestScrollSpeed; i <= Constant.Play.FastestScrollSpeed; i += Constant.Play.ScrollSpeedUnit)
			{
				var scrollSpeed = LocalizeLoader.Instance.Format("ScrollSpeedFormat", (i / 100.0).ToString("n2"), multiplier);

				horizontalListView.AddItemValue
				(
					"scrollSpeed",
					new HorizontalListViewItem
					(
						scrollSpeed,
						i,
						LocalizeLoader.Instance.Find("ScrollSpeedDescription")
					)
				);
			}

			horizontalListView.SelectInSelectionByIndex
			(
				"scrollSpeed",
				horizontalListView.FindIndexInSelectionByKey("scrollSpeed", playerConfiguration.scrollSpeed)
			);
			horizontalListView.UpdateSelection("scrollSpeed");
		}

		/// <summary>
		/// 次の項目を選択する
		/// </summary>
		public void SelectNext()
		{
			horizontalListView.SelectNext();
		}

		/// <summary>
		/// 前の項目を選択する
		/// </summary>
		public void SelectBefore()
		{
			horizontalListView.SelectBefore();
		}

		/// <summary>
		/// 選択されている項目の次の値を選択する
		/// </summary>
		public void SelectNextInSelection()
		{
			horizontalListView.SelectNextInSelection();
		}

		/// <summary>
		/// 選択されている項目の前の値を選択する
		/// </summary>
		public void SelectBeforeInSelection()
		{
			horizontalListView.SelectBeforeInSelection();
		}

		public void OnEnable()
		{
			mask.SetActive(true);
		}

		public void OnDisable()
		{
			var savedPlayerConfiguration = new PlayerConfiguration();

			savedPlayerConfiguration.scrollSpeed = horizontalListView.GetSelectedValueInSelectionAsNumber("scrollSpeed");
			savedPlayerConfiguration.clearReference = horizontalListView.GetSelectedValueInSelectionAsString("clearReference");

			if((playerConfiguration.clearReference != savedPlayerConfiguration.clearReference) || (playerConfiguration.scrollSpeed != savedPlayerConfiguration.scrollSpeed))
			{
				// 設定に差分があれば保存する
				configuration.Save(savedPlayerConfiguration);	
				playerConfiguration = savedPlayerConfiguration;
			}

			mask.SetActive(false);
		}
	}
}