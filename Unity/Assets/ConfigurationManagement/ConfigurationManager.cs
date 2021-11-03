using System.IO;
using System.Collections;
using System.Collections.Generic;
using ResourceLoader;
using UnityEngine;

namespace ConfigurationManagement
{
	/// <summary>
	/// �ݒ���Ǘ�����N���X
	/// </summary>
	public class ConfigurationManager<T>
	{
		/// <summary>
		/// �ݒ��ۑ�����t�@�C���p�X
		/// </summary>
		private string filePath;

		/// <summary>
		/// �ݒ�
		/// </summary>
		private T configuration;

		private const bool RewritesConfigurationFile = false;

		/// <summary>
		/// �R���X�g���N�^
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
		/// �t�@�C����ۑ�����
		/// </summary>
		/// <param name="configuration">�ݒ�f�[�^</param>
		public void Save(T configuration)
		{
			var json = JsonUtility.ToJson(configuration);
			var streamWriter = new StreamWriter(filePath, RewritesConfigurationFile, System.Text.Encoding.UTF8);
			streamWriter.WriteLine(json);
			streamWriter.Flush();
			streamWriter.Close();
		}

		/// <summary>
		/// �ݒ�f�[�^��ǂݍ���
		/// </summary>
		public T Load()
		{
			return configuration;
		}
	}
}