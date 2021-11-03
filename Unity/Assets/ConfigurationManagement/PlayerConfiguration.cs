using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement
{
	[Serializable]
	class PlayerConfiguration
	{
		public string clearReference;
		public int scrollSpeed;
		/*
		/// <summary>
		/// クラスの==比較演算子
		/// </summary>
		/// <param name="source">比較元の設定</param>
		/// <param name="destination">比較先の設定</param>
		public static bool operator == (PlayerConfiguration source, PlayerConfiguration destination)
		{
			if(ReferenceEquals(source, destination))
			{
				return true;
			}

			if((object)source == null || (object)destination == null)
			{
				return false;
			}

			return source.clearReference == destination.clearReference && source.scrollSpeed == destination.scrollSpeed;
		}

		/// <summary>
		/// クラスの!=比較演算子
		/// </summary>
		/// <param name="source">比較元の設定</param>
		/// <param name="destination">比較先の設定</param>
		public static bool operator != (PlayerConfiguration source, PlayerConfiguration destination)
		{
			return !(source == destination);
		}
		*/
	}
}
