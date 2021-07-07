using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Localization;
using ResourceLoader;

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
            var filePath = Directory.GetCurrentDirectory();
#else
            var filePath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd("\\");
#endif

            // �Ƃ肠�������ŉp��̌���ݒ�ɂ��Ă���
            var jsonFilePath = string.Format("{0}\\Languages\\en-US.json", filePath);
            var jsonReader = new ResourceLoader.TextLoader(jsonFilePath);
            LocalizeLoader.Instance.Initialize(new LocalizeAnalyzer(jsonReader));
            LocalizeLoader.Instance.SetLocale("en-US");

            var sqlserver = new SQLiteServer.SQLiteServer();
            sqlserver.Start(filePath, "game.db");

            //UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
    }
}