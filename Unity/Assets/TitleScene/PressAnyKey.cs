using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        private TitleAction action;


        public void Awake()
        {
            action = new TitleAction();
            action.Enable();

            sceneExitTriggerStateHash = Animator.StringToHash(sceneExitTriggerStateTagName);
            action.UI.Proceed.performed += context =>
            {
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

            if(allAnimationEnds)
            {

            }
        }
    }
}
