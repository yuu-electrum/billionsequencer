using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace TitleScene
{
    /// <summary>
    /// タイトル画面で何かの操作がされた時に反応するスクリプト
    /// </summary>
    public class PressAnyKey: MonoBehaviour
    {
        [SerializeField]
        private Animator[] animators;

        [SerializeField]
        private string[] sceneExitTriggerStateNames;

        [SerializeField]
        private string sceneExitTriggerStateTagName;

        [SerializeField]
        private int sceneExitTriggerStateHash;

        [SerializeField]
        private AudioListener audioListener;

        [SerializeField]
        private InputSystemUIInputModule inputModule;

        [SerializeField]
        private EventSystem eventSystem;

        private TitleAction action;
        private bool isNextSceneLoadingDone;

        public void Awake()
        {
            action = new TitleAction();
            action.Enable();
            isNextSceneLoadingDone = false;

            sceneExitTriggerStateHash = Animator.StringToHash(sceneExitTriggerStateTagName);
            action.UI.Proceed.performed += context =>
            {
                action.Disable();
                StartCoroutine(LoadNextSceneAsync());
                // 何かキーが押された時の動作
                foreach(var animator in animators)
                {
                    animator.SetBool("WillProceed", true);
                }
            };
        }

        public void Update()
        {
            var allAnimationEnds = true;
            foreach(var animator in animators)
            {
                allAnimationEnds = animator.GetCurrentAnimatorStateInfo(0).tagHash == sceneExitTriggerStateHash && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
            }

            if(allAnimationEnds && isNextSceneLoadingDone)
            {
                StartCoroutine(UnloadSceneAsync());
            }
        }

        /// <summary>
        /// 次のシーンを非同期で読み込む
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadNextSceneAsync()
        {
            yield return SceneManager.LoadSceneAsync("ChartSelectScene", LoadSceneMode.Additive);

            Destroy(audioListener);
            Destroy(inputModule);
            Destroy(eventSystem);
            isNextSceneLoadingDone = true;
        }

        private IEnumerator UnloadSceneAsync()
        {
            yield return SceneManager.UnloadSceneAsync("TitleScene");
        }
    }
}
