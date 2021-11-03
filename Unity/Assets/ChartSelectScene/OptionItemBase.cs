using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChartSelectScene
{
    /// <summary>
    /// 選択肢のベース
    /// </summary>
	public class OptionItemBase: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI itemLabel;

        [SerializeField]
        private TextMeshProUGUI itemValue;

        [SerializeField]
        private Image leftArrow;

        [SerializeField]
        private Image rightArrow;

        /// <summary>
        /// 項目を選択された状態にする
        /// </summary>
        public void Select()
        {
            var selectedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            itemLabel.color = selectedColor;
            itemValue.color = selectedColor;
            leftArrow.color = selectedColor;
            rightArrow.color = selectedColor;
        }

        /// <summary>
        /// 項目の選択を解除された状態にする
        /// </summary>
		public void Deselect()
        {
            var deselectedColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            itemLabel.color = deselectedColor;
            itemValue.color = deselectedColor;
            leftArrow.color = deselectedColor;
            rightArrow.color = deselectedColor;
        }

        public string ItemLabel
        {
            get
            {
                return itemLabel.text;
            }

            set
            {
                itemLabel.text = value;
            }
        }

        public string ItemValue
        {
            get
            {
                return itemValue.text;
            }

            set
            {
                itemValue.text = value;
            }
        }
    }
}