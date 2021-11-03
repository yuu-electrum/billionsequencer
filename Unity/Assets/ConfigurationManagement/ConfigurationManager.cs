using System.IO;
using System.Collections;
using System.Collections.Generic;
using ResourceLoader;
using UnityEngine;

namespace ConfigurationManagement
{
	/// <summary>
	/// 設定を管理するクラス
	/// </summary>
	public class ConfigurationManager<T>
	{
		/// <summary>
		/// 設定を保存するファイルパス
		/// </summary>
		private string filePath;

		/// <summary>
		/// 設定
		/// </summary>
		private T configuration;

		private const bool RewritesConfigurationFile = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ConfigurationManager(string filePath, T defaultConfiguration)
		{
			this.filePath = filePath;
			if(!File.Exists(filePath))
			{
				Save(defaultConfiguration);
			}

			var loader = new TextLoader(filePath);
			var json = loader.ReadAll();
			
			configuration = JsonUtility.FromJson<T>(json);
		}

		/// <summary>
		/// ファイルを保存する
		/// </summary>
		/// <param name="configuration">設定データ</param>
		public void Save(T configuration)
		{
			var json = JsonUtility.ToJson(configuration);
			var streamWriter = new StreamWriter(filePath, RewritesConfigurationFile, System.Text.Encoding.UTF8);
			streamWriter.WriteLine(json);
			streamWriter.Flush();
			streamWriter.Close();
		}

		/// <summary>
		/// 設定データを読み込む
		/// </summary>
		public T Load()
		{
			return configuration;
		}
	}
}