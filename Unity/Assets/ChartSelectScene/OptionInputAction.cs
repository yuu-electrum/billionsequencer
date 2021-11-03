// GENERATED AUTOMATICALLY FROM 'Assets/ChartSelectScene/OptionInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ChartSelectScene
{
    public class @OptionInputAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @OptionInputAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""OptionInputAction"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""94fd81ae-5278-416f-96f6-5ffa838ef3f3"",
            ""actions"": [
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""62cb3e35-fd8f-4cd5-af20-6c5ea0d48b29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""32b2b5c7-120d-490b-8868-77f02446706c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""e1e641f3-d82c-451d-941b-2b4790ab8d43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""4a9c430b-6bfb-4d28-8d4b-41346242c578"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""5312e557-3ac9-4f59-b7aa-0f47cb705ce8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""49e71cb4-c44e-4d92-afd1-c1dca2d2ac24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""de983dc5-823c-4d85-931d-3e20a28ca265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9afd9e1c-828d-4d31-a45a-2a271b3d40ad"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba6ec488-c74b-4a1d-9d40-fded391aeb22"",
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
                    ""id"": ""516985d4-4a7d-4938-b8ec-0e58d70a2727"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70b2a00d-ac50-4481-b9fe-2bf15d9ca1e8"",
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
                    ""id"": ""84c7c0a2-63b1-4f9a-b23d-cf74e341f00c"",
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
                    ""id"": ""11b20d6f-f485-4317-8043-bb066532ecdb"",
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
                    ""id"": ""bbd93eb5-227b-4687-8c58-bd17ba6a8c24"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c19dafb-66fd-4034-9a65-b249a3415132"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
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
            m_UI_Tab = m_UI.FindAction("Tab", throwIfNotFound: true);
            m_UI_Left = m_UI.FindAction("Left", throwIfNotFound: true);
            m_UI_Right = m_UI.FindAction("Right", throwIfNotFound: true);
            m_UI_Up = m_UI.FindAction("Up", throwIfNotFound: true);
            m_UI_Down = m_UI.FindAction("Down", throwIfNotFound: true);
            m_UI_Enter = m_UI.FindAction("Enter", throwIfNotFound: true);
            m_UI_Quit = m_UI.FindAction("Quit", throwIfNotFound: true);
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
        private readonly InputAction m_UI_Tab;
        private readonly InputAction m_UI_Left;
        private readonly InputAction m_UI_Right;
        private readonly InputAction m_UI_Up;
        private readonly InputAction m_UI_Down;
        private readonly InputAction m_UI_Enter;
        private readonly InputAction m_UI_Quit;
        public struct UIActions
        {
            private @OptionInputAction m_Wrapper;
            public UIActions(@OptionInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Tab => m_Wrapper.m_UI_Tab;
            public InputAction @Left => m_Wrapper.m_UI_Left;
            public InputAction @Right => m_Wrapper.m_UI_Right;
            public InputAction @Up => m_Wrapper.m_UI_Up;
            public InputAction @Down => m_Wrapper.m_UI_Down;
            public InputAction @Enter => m_Wrapper.m_UI_Enter;
            public InputAction @Quit => m_Wrapper.m_UI_Quit;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Tab.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                    @Tab.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                    @Tab.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                    @Left.started -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Left.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnLeft;
                    @Right.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Right.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Right.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRight;
                    @Up.started -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Up.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnUp;
                    @Down.started -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Down.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnDown;
                    @Enter.started -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Enter.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Enter.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnEnter;
                    @Quit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                    @Quit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                    @Quit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Tab.started += instance.OnTab;
                    @Tab.performed += instance.OnTab;
                    @Tab.canceled += instance.OnTab;
                    @Left.started += instance.OnLeft;
                    @Left.performed += instance.OnLeft;
                    @Left.canceled += instance.OnLeft;
                    @Right.started += instance.OnRight;
                    @Right.performed += instance.OnRight;
                    @Right.canceled += instance.OnRight;
                    @Up.started += instance.OnUp;
                    @Up.performed += instance.OnUp;
                    @Up.canceled += instance.OnUp;
                    @Down.started += instance.OnDown;
                    @Down.performed += instance.OnDown;
                    @Down.canceled += instance.OnDown;
                    @Enter.started += instance.OnEnter;
                    @Enter.performed += instance.OnEnter;
                    @Enter.canceled += instance.OnEnter;
                    @Quit.started += instance.OnQuit;
                    @Quit.performed += instance.OnQuit;
                    @Quit.canceled += instance.OnQuit;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnTab(InputAction.CallbackContext context);
            void OnLeft(InputAction.CallbackContext context);
            void OnRight(InputAction.CallbackContext context);
            void OnUp(InputAction.CallbackContext context);
            void OnDown(InputAction.CallbackContext context);
            void OnEnter(InputAction.CallbackContext context);
            void OnQuit(InputAction.CallbackContext context);
        }
    }
}
