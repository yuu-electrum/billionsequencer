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

        [SerializeField]
        private ChartListView listView;

        public void Start()
        {
            chartSelectAction = new ChartSelectAction();
            chartSelectAction.Enable();

            chartSelectAction.UI.Down.performed += context => { listView.MoveSelection(ChartListView.MoveSelectionDirection.Next, 1); };
            chartSelectAction.UI.Up.performed += context => { listView.MoveSelection(ChartListView.MoveSelectionDirection.Before, 1); };
            chartSelectAction.UI.Enter.performed += context => { listView.ExecuteSelection(); };
            chartSelectAction.UI.Left.performed += context => { listView.ShowFolderList(); };
        }

        public void OnDestroy()
        {
            chartSelectAction.Disable();
        }
    }
}