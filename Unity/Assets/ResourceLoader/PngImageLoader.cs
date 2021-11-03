using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Drawing;

namespace ResourceLoader
{
	/// <summary>
	/// PNG画像を動的に読み込むインターフェース
	/// </summary>
	public class PngImageLoader : IDynamicImageLoader
	{
		private bool isLoaded;
		private Texture2D texture;
		private Size textureSize;
		private const int ImageSizeByteStartsAt = 16;
		private const int ImageSizeByteLength = 4;

		public PngImageLoader()
		{
			isLoaded = false;
		}

		public IEnumerator Load(string imageFilePath)
		{
			if(!File.Exists(imageFilePath))
			{
				yield break;
			}

			var stream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
			var binary = new BinaryReader(stream);

			byte[] bytes = binary.ReadBytes((int)binary.BaseStream.Length);
			binary.Close();

			int width = 0;
			int height = 0;

			for (int i = ImageSizeByteStartsAt; i < ImageSizeByteStartsAt + ImageSizeByteLength; i++)
			{
				width = width * 256 + bytes[i];
				height = height * 256 + bytes[i + ImageSizeByteLength];
			}

			textureSize.Width = width;
			textureSize.Height = height;

			texture = new Texture2D(width, height);
			isLoaded = texture.LoadImage(bytes);

			yield break;
		}

		public Texture2D Image
		{
			get
			{
				return !IsLoaded ? null : texture;
			}
		}

		public int Width
		{
			get
			{
				return !IsLoaded ? -1 : textureSize.Width;
			}
		}

		public int Height
		{
			get
			{
				return !IsLoaded ? -1 : textureSize.Height;
			}
		}

		public bool IsLoaded
		{
			get
			{
				return isLoaded;
			}
		}
	}
}