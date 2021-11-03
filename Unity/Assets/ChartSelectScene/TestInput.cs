using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChartSelectScene
{
    public class TestInput: MonoBehaviour
    {
        private ChartSelectAction chartSelectAction;
        private OptionInputAction optionInputAction;

        [SerializeField]
        private ChartListView listView;

        [SerializeField]
        private OptionView optionView;

        public void Start()
        {
            chartSelectAction = new ChartSelectAction();
            chartSelectAction.Enable();

            optionInputAction = new OptionInputAction();

            chartSelectAction.UI.Down.performed += context => { listView.MoveSelection(ChartListView.MoveSelectionDirection.Next, 1); };
            chartSelectAction.UI.Up.performed += context => { listView.MoveSelection(ChartListView.MoveSelectionDirection.Before, 1); };
            chartSelectAction.UI.Enter.performed += context => { listView.ExecuteSelection(); };
            chartSelectAction.UI.Left.performed += context => { listView.ShowFolderList(); };
            chartSelectAction.UI.Tab.performed += context => {
                optionInputAction.Enable();
                chartSelectAction.Disable();
                listView.HideInactiveHiddenObjects();
                optionView.gameObject.SetActive(true);
            };

            optionInputAction.UI.Quit.performed += context => {
                chartSelectAction.Enable();
                optionInputAction.Disable();
                listView.ShowInactiveHiddenObjects();
                optionView.gameObject.SetActive(false);
            };
            optionInputAction.UI.Down.performed += context => {
                optionView.SelectNext();
            };
            optionInputAction.UI.Up.performed += context => {
                optionView.SelectBefore();
            };
            optionInputAction.UI.Left.performed += context => {
                optionView.SelectBeforeInSelection();
            };
            optionInputAction.UI.Right.performed += context => {
                optionView.SelectNextInSelection();
            };
        }

        public void OnDestroy()
        {
            chartSelectAction.Disable();
            optionInputAction.Disable();
        }
    }
}