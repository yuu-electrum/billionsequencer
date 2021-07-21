using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChartSelectScene
{
    public class TestInput: MonoBehaviour
    {
        [SerializeField]
        private ChartSelectAction chartSelectAction;

        public void Start()
        {
            /*
            chartSelectAction = new ChartSelectAction();
            chartSelectAction.Enable();

            chartSelectAction.UI.Down.performed += context => { listView.MoveCursorToDown(); listView.Layout(); };
            chartSelectAction.UI.Up.performed += context => { listView.MoveCursorToUp(); listView.Layout(); };
            */
        }

        public void OnDestroy()
        {
            //chartSelectAction.Disable();
        }
    }
}