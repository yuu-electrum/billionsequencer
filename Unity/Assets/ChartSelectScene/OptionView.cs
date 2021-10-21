using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using TMPro;

namespace ChartSelectScene
{
	/// <summary>
	/// �I�v�V�����r���[
	/// </summary>
	public class OptionView: MonoBehaviour
	{
		/// <summary>
		/// �N���A�
		/// </summary>
		public enum GameSuccessReference
		{
			/// <summary>
			/// �����^�Q�[�W
			/// </summary>
			OverReference,

			/// <summary>
			/// �����^�Q�[�W
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
		/// ���ʂ̃X�N���[���X�s�[�h
		/// </summary>
		public int ScrollSpeed
		{
			get
			{
				return scrollSpeedPercentage;
			}
		}

		/// <summary>
		/// �N���A�
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