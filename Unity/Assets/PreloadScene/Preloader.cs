using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PreloadScene
{
    /// <summary>
    /// �Q�[���J�n�O�̓ǂݍ��݃N���X
    /// </summary>
    public class Preloader: MonoBehaviour
    {
        public void Start()
        {
            // �Q�[���̎��s����Ă���f�B���N�g�����擾����
#if UNITY_EDITOR
            var filePath = Directory.GetCurrentDirectory().Replace("\\", "/");
#else
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\").Replace("\\", "/");
#endif

            // �Ƃ肠�������ŉp��̌���ݒ�ɂ��Ă���
            Localization.LocalizeLoader.Instance.Load(string.Format("{0}/Languages/en-US.json", filePath));
            Localization.LocalizeLoader.Instance.SetLocale("en-US");

            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}