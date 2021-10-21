using System.Collections;
using System.Collections.Generic;
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
			horizontalListView.AddItem("hoge", "fuga", "aaaa");
			horizontalListView.AddItem("aaaa", "fuga", "aaaa");
		}

		public void OnEnable()
		{
			mask.SetActive(true);
		}

		public void OnDisable()
		{
			mask.SetActive(false);
		}
	}
}