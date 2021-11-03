using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChartSelectScene
{
    public class ListViewItem : MonoBehaviour
    {
        private Animator selfAnimator;
        private bool isAnimationForcedComplete;
        private float normalizedTime;
        private const string FadeinStateName = "New State";

        public void Start()
        {
            selfAnimator = this.GetComponent<Animator>();
            isAnimationForcedComplete = false;
        }
		public void OnEnable()
		{
            if(isAnimationForcedComplete && selfAnimator)
            {
                selfAnimator.Play(FadeinStateName, 0, normalizedTime);
            }
		}

		/// <summary>
		/// Animator‚ðŠ®—¹‚³‚¹‚é
		/// </summary>
		public void FinishAnimator()
        {
            if(!selfAnimator || isAnimationForcedComplete)
            {
                return;
            }

            normalizedTime = 1.0f;
            isAnimationForcedComplete = true;
        }
    }
}