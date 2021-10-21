// GENERATED AUTOMATICALLY FROM 'Assets/ChartSelectScene/ChartSelectAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ChartSelectScene
{
    public class @ChartSelectAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @ChartSelectAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""ChartSelectAction"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""235bbf52-fac0-4039-a44b-2c7442bb5545"",
            ""actions"": [
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""b0d5c7f2-e739-40dc-9c24-58e6624efffa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""34301c57-bc53-4522-84ff-5cbc945ef189"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""06aa1829-42e9-445c-b47b-cd52ec3f8514"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""08a6fb03-4857-4a5c-9e11-66ac1cc3403f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""e4b10b14-4d0c-499b-9d92-8354f684307e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""032a8e84-23a4-4b8c-916e-71df520b37b3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1b8e6e9-ed3f-4758-99f6-416057098ec9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4de51f3-0848-44bf-bcc0-195acff9fa5e"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e27855d-c34f-4b88-981c-ef102e063e5f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3928ab9d-3c1a-43b0-9a79-8d3505862dcc"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Down = m_UI.FindAction("Down", throwIfNotFound: true);
            m_UI_Up = m_UI.FindAction("Up", throwIfNotFound: true);
            m_UI_Enter = m_UI.FindAction("Enter", throwIfNotFound: true);
            m_UI_Left = m_UI.FindAction("Left", throwIfNotFound: true);
            m_UI_Tab = m_UI.FindAction("Tab", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Down;
        private readonly InputAction m_UI_Up;
        private readonly InputAction m_UI_Enter;
        private readonly InputAction m_UI_Left;
        private readonly InputAction m_UI_Tab;
        public struct UIActions
        {
            private @ChartSelectAction m_Wrapper;
            public UIActions(@ChartSelectAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Down => m_Wrapper.m_UI_Down;
            public InputAction @Up => m_Wrapper.m_UI_Up;
            public InputAction @Enter => m_Wrapper.m_UI_Enter;
            public InputAction @Left => m_Wrapper.m_UI_Left;
            public InputAction @Tab => m_Wrapper.m_UI_Tab;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Down.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Up.started -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Enter.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Enter.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Enter.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Left.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Tab.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                    @Tab.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                    @Tab.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Down.started += instance.OnDown;
                    @Down.performed += instance.OnDown;
                    @Down.canceled += instance.OnDown;
                    @Up.started += instance.OnUp;
                    @Up.performed += instance.OnUp;
                    @Up.canceled += instance.OnUp;
                    @Enter.started += instance.OnEnter;
                    @Enter.performed += instance.OnEnter;
                    @Enter.canceled += instance.OnEnter;
                    @Left.started += instance.OnLeft;
                    @Left.performed += instance.OnLeft;
                    @Left.canceled += instance.OnLeft;
                    @Tab.started += instance.OnTab;
                    @Tab.performed += instance.OnTab;
                    @Tab.canceled += instance.OnTab;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnDown(InputAction.CallbackContext context);
            void OnUp(InputAction.CallbackContext context);
            void OnEnter(InputAction.CallbackContext context);
            void OnLeft(InputAction.CallbackContext context);
            void OnTab(InputAction.CallbackContext context);
        }
    }
}
