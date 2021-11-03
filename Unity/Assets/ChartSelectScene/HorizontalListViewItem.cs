using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartSelectScene
{
	/// <summary>
	/// 横方向のリストビューアイテム
	/// </summary>
	public class HorizontalListViewItem
	{
		private ValueType valueType;
		private string label;
		private string stringValue;
		private int numberValue;
		private double realValue;
		private string description;

		/// <summary>
		/// リストビューアイテムのデータ型
		/// </summary>
		public enum ValueType
		{
			String, Number, Real
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="label">値を識別する値</param>
		/// <param name="value">値</param>
		public HorizontalListViewItem(string label, string value, string description)
		{
			Label = label;
			StringValue = value;
			Description = description;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="label">値を識別する値</param>
		/// <param name="value">値</param>
		public HorizontalListViewItem(string label, int value, string description)
		{
			Label = label;
			NumberValue = value;
			Description = description;
		}

		/// <summary>
		/// 画面に表示されるラベル
		/// </summary>
		public string Label
		{
			get
			{
				return label;
			}

			set
			{
				label = value;
			}
		}

		/// <summary>
		/// 文字列型の値
		/// </summary>
		public string StringValue
		{
			get
			{
				return stringValue;
			}

			set
			{
				stringValue = value;
			}
		}

		/// <summary>
		/// 数値型の値
		/// </summary>
		public int NumberValue
		{
			get
			{
				return numberValue;
			}

			set
			{
				numberValue = value;
			}
		}

		/// <summary>
		/// 選択肢の説明
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}

			set
			{
				description = value;
			}
		}
	}
}
