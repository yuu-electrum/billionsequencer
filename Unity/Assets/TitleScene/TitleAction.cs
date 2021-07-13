// GENERATED AUTOMATICALLY FROM 'Assets/TitleScene/TitleAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace TitleScene
{
    public class @TitleAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @TitleAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""TitleAction"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""894f523e-4ba1-48a4-8645-6bed2a0b2f2c"",
            ""actions"": [
                {
                    ""name"": ""Proceed"",
                    ""type"": ""Button"",
                    ""id"": ""0ab1f17d-e52a-48cc-8d1b-6e89e820b2fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f59762c3-8cb7-4355-97c2-5d7a8a0a8bf7"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Proceed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cdc88c79-178b-476e-abba-d218a4247a00"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Proceed"",
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
            m_UI_Proceed = m_UI.FindAction("Proceed", throwIfNotFound: true);
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
        private readonly InputAction m_UI_Proceed;
        public struct UIActions
        {
            private @TitleAction m_Wrapper;
            public UIActions(@TitleAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Proceed => m_Wrapper.m_UI_Proceed;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Proceed.started -= m_Wrapper.m_UIActionsCallbackInterface.OnProceed;
                    @Proceed.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnProceed;
                    @Proceed.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnProceed;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Proceed.started += instance.OnProceed;
                    @Proceed.performed += instance.OnProceed;
                    @Proceed.canceled += instance.OnProceed;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IUIActions
        {
            void OnProceed(InputAction.CallbackContext context);
        }
    }
}
