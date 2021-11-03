using System.Collections;
using System.Collections.Generic;
using ConfigurationManagement;
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

		private ConfigurationManager<PlayerConfiguration> configuration;
		private PlayerConfiguration playerConfiguration;

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
			// �ݒ�t�@�C�������݂��Ȃ��ꍇ�̃f�t�H���g�̐ݒ���쐬����
			var defaultConfiguration = new PlayerConfiguration();
			defaultConfiguration.clearReference = "over_reference";
			defaultConfiguration.scrollSpeed = 100;
			
			configuration = new ConfigurationManager<PlayerConfiguration>
			(
				Constant.Path.WorkingDirectory + Constant.Path.ConfigurationFilePath,
				defaultConfiguration
			);
			playerConfiguration = configuration.Load();

			// �ݒ荀�ڂ�ǉ�����
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

			// �N���A�
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

			// �X�N���[���X�s�[�h
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
		/// ���̍��ڂ�I������
		/// </summary>
		public void SelectNext()
		{
			horizontalListView.SelectNext();
		}

		/// <summary>
		/// �O�̍��ڂ�I������
		/// </summary>
		public void SelectBefore()
		{
			horizontalListView.SelectBefore();
		}

		/// <summary>
		/// �I������Ă��鍀�ڂ̎��̒l��I������
		/// </summary>
		public void SelectNextInSelection()
		{
			horizontalListView.SelectNextInSelection();
		}

		/// <summary>
		/// �I������Ă��鍀�ڂ̑O�̒l��I������
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
				// �ݒ�ɍ���������Εۑ�����
				configuration.Save(savedPlayerConfiguration);	
				playerConfiguration = savedPlayerConfiguration;
			}

			mask.SetActive(false);
		}
	}
}