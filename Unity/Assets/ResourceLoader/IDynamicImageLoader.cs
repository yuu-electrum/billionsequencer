using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoader
{
	/// <summary>
	/// �摜���f�B�X�N���瓮�I�ɓǂݍ��ރC���^�[�t�F�[�X
	/// </summary>
	public interface IDynamicImageLoader
	{
		/// <summary>
		/// �C���[�W��ǂݍ���
		/// </summary>
		/// <param name="imageFilePath">�摜�p�X</param>
		public IEnumerator Load(string imageFilePath);

		/// <summary>
		/// �C���[�W���擾����
		/// </summary>
		public Texture2D Image { get; }

		/// <summary>
		/// �C���[�W�̕����擾����
		/// </summary>
		public int Width { get; }

		/// <summary>
		/// �C���[�W�̍������擾����
		/// </summary>
		public int Height { get; }

		/// <summary>
		/// �摜���ǂݍ��܂�Ă��邩�擾����
		/// </summary>
		public bool IsLoaded { get; }
	}
}