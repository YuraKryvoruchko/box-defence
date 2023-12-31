// GENERATED AUTOMATICALLY FROM 'Assets/Level/Inputs/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Touch"",
            ""id"": ""01eb55ba-d5e8-4b59-97bb-4d1e9202c4ad"",
            ""actions"": [
                {
                    ""name"": ""TouchDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""16aac1a7-ed36-4f5a-88d2-27e1c3332a2f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchTap"",
                    ""type"": ""Button"",
                    ""id"": ""54ba538d-a007-486a-9dff-0be90916d6c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d44c955-9305-4b45-93a7-4c1c03225e1d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchFirstPosition"",
                    ""type"": ""Value"",
                    ""id"": ""fcb814a0-1d90-4f09-ad8e-04c4c88926fb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""TouchSecondPosition"",
                    ""type"": ""Value"",
                    ""id"": ""e177d6ef-96f2-4093-9d51-28763a3dc489"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchSecondContact"",
                    ""type"": ""Button"",
                    ""id"": ""52a53d47-56fe-4fae-80c3-3387ff69bb75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""809a4e45-d497-43b0-a0cb-cf6a2c97ff7c"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06d2380b-5448-4bc6-9a1d-020995560c53"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchTap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fff8d70-170a-4f03-87df-f98cd476ddb7"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d22e7539-892a-430c-b706-069ef23219ee"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchSecondPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b9c3a50-a019-47e7-8b7a-8159b7a137f7"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchSecondContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d3ea425-a05c-4351-974c-79fe70b1a399"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchFirstPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Touch
        m_Touch = asset.FindActionMap("Touch", throwIfNotFound: true);
        m_Touch_TouchDelta = m_Touch.FindAction("TouchDelta", throwIfNotFound: true);
        m_Touch_TouchTap = m_Touch.FindAction("TouchTap", throwIfNotFound: true);
        m_Touch_TouchPosition = m_Touch.FindAction("TouchPosition", throwIfNotFound: true);
        m_Touch_TouchFirstPosition = m_Touch.FindAction("TouchFirstPosition", throwIfNotFound: true);
        m_Touch_TouchSecondPosition = m_Touch.FindAction("TouchSecondPosition", throwIfNotFound: true);
        m_Touch_TouchSecondContact = m_Touch.FindAction("TouchSecondContact", throwIfNotFound: true);
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

    // Touch
    private readonly InputActionMap m_Touch;
    private ITouchActions m_TouchActionsCallbackInterface;
    private readonly InputAction m_Touch_TouchDelta;
    private readonly InputAction m_Touch_TouchTap;
    private readonly InputAction m_Touch_TouchPosition;
    private readonly InputAction m_Touch_TouchFirstPosition;
    private readonly InputAction m_Touch_TouchSecondPosition;
    private readonly InputAction m_Touch_TouchSecondContact;
    public struct TouchActions
    {
        private @InputSystem m_Wrapper;
        public TouchActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @TouchDelta => m_Wrapper.m_Touch_TouchDelta;
        public InputAction @TouchTap => m_Wrapper.m_Touch_TouchTap;
        public InputAction @TouchPosition => m_Wrapper.m_Touch_TouchPosition;
        public InputAction @TouchFirstPosition => m_Wrapper.m_Touch_TouchFirstPosition;
        public InputAction @TouchSecondPosition => m_Wrapper.m_Touch_TouchSecondPosition;
        public InputAction @TouchSecondContact => m_Wrapper.m_Touch_TouchSecondContact;
        public InputActionMap Get() { return m_Wrapper.m_Touch; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TouchActions set) { return set.Get(); }
        public void SetCallbacks(ITouchActions instance)
        {
            if (m_Wrapper.m_TouchActionsCallbackInterface != null)
            {
                @TouchDelta.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchDelta;
                @TouchDelta.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchDelta;
                @TouchDelta.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchDelta;
                @TouchTap.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchTap;
                @TouchTap.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchTap;
                @TouchTap.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchTap;
                @TouchPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchPosition;
                @TouchFirstPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchFirstPosition;
                @TouchFirstPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchFirstPosition;
                @TouchFirstPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchFirstPosition;
                @TouchSecondPosition.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondPosition;
                @TouchSecondPosition.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondPosition;
                @TouchSecondPosition.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondPosition;
                @TouchSecondContact.started -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondContact;
                @TouchSecondContact.performed -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondContact;
                @TouchSecondContact.canceled -= m_Wrapper.m_TouchActionsCallbackInterface.OnTouchSecondContact;
            }
            m_Wrapper.m_TouchActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TouchDelta.started += instance.OnTouchDelta;
                @TouchDelta.performed += instance.OnTouchDelta;
                @TouchDelta.canceled += instance.OnTouchDelta;
                @TouchTap.started += instance.OnTouchTap;
                @TouchTap.performed += instance.OnTouchTap;
                @TouchTap.canceled += instance.OnTouchTap;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @TouchFirstPosition.started += instance.OnTouchFirstPosition;
                @TouchFirstPosition.performed += instance.OnTouchFirstPosition;
                @TouchFirstPosition.canceled += instance.OnTouchFirstPosition;
                @TouchSecondPosition.started += instance.OnTouchSecondPosition;
                @TouchSecondPosition.performed += instance.OnTouchSecondPosition;
                @TouchSecondPosition.canceled += instance.OnTouchSecondPosition;
                @TouchSecondContact.started += instance.OnTouchSecondContact;
                @TouchSecondContact.performed += instance.OnTouchSecondContact;
                @TouchSecondContact.canceled += instance.OnTouchSecondContact;
            }
        }
    }
    public TouchActions @Touch => new TouchActions(this);
    public interface ITouchActions
    {
        void OnTouchDelta(InputAction.CallbackContext context);
        void OnTouchTap(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnTouchFirstPosition(InputAction.CallbackContext context);
        void OnTouchSecondPosition(InputAction.CallbackContext context);
        void OnTouchSecondContact(InputAction.CallbackContext context);
    }
}
