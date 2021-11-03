using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
	/// <summary>
	/// 画像をディスクから動的に読み込むインターフェース
	/// </summary>
	public interface IDynamicImageLoader
	{
		/// <summary>
		/// イメージを読み込む
		/// </summary>
		/// <param name="imageFilePath">画像パス</param>
		public IEnumerator Load(string imageFilePath);

		/// <summary>
		/// イメージを取得する
		/// </summary>
		public Texture2D Image { get; }

		/// <summary>
		/// イメージの幅を取得する
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// イメージの高さを取得する
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// 画像が読み込まれているか取得する
		/// </summary>
		public bool IsLoaded { get; }
	}
}