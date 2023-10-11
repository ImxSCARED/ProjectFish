//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Input/InputSystem/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""ship"",
            ""id"": ""6a2b60fa-e516-408d-a67a-5bf3c7dfde75"",
            ""actions"": [
                {
                    ""name"": ""forward"",
                    ""type"": ""Value"",
                    ""id"": ""ac4d348b-03f8-447f-abb9-b23641e3ba96"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""yaw"",
                    ""type"": ""Value"",
                    ""id"": ""3cc2585c-00e9-495a-915e-d1b3b15e44bb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2064f02b-e8d5-4f97-8b2a-3534b984dc33"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Playstation DualShock controller;Xbox controller;Nintendo Pro controller"",
                    ""action"": ""yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""592512ae-f1f1-4451-afa7-77d083496edb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard (Testing)"",
                    ""action"": ""yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9536634c-222a-4d17-bc31-8985e7240d8b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard (Testing)"",
                    ""action"": ""yaw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a67d66ec-aa99-4d70-b871-c3b076c20d2f"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox controller;Nintendo Pro controller;Playstation DualShock controller"",
                    ""action"": ""forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36a73862-2df7-4acb-98a7-7dbc4e70728d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard (Testing)"",
                    ""action"": ""forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""935370e3-3055-4ad6-9451-cec9a08da94f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard (Testing)"",
                    ""action"": ""forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""cannon"",
            ""id"": ""912df182-72a1-47d3-964b-8d1412da2ea7"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""menu"",
            ""id"": ""c5541d30-f4d3-4e69-8569-198f8b81c856"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Playstation DualShock controller"",
            ""bindingGroup"": ""Playstation DualShock controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox controller"",
            ""bindingGroup"": ""Xbox controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Nintendo Pro controller"",
            ""bindingGroup"": ""Nintendo Pro controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<SwitchProControllerHID>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard (Testing)"",
            ""bindingGroup"": ""Keyboard (Testing)"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ship
        m_ship = asset.FindActionMap("ship", throwIfNotFound: true);
        m_ship_forward = m_ship.FindAction("forward", throwIfNotFound: true);
        m_ship_yaw = m_ship.FindAction("yaw", throwIfNotFound: true);
        // cannon
        m_cannon = asset.FindActionMap("cannon", throwIfNotFound: true);
        // menu
        m_menu = asset.FindActionMap("menu", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ship
    private readonly InputActionMap m_ship;
    private IShipActions m_ShipActionsCallbackInterface;
    private readonly InputAction m_ship_forward;
    private readonly InputAction m_ship_yaw;
    public struct ShipActions
    {
        private @PlayerActions m_Wrapper;
        public ShipActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @forward => m_Wrapper.m_ship_forward;
        public InputAction @yaw => m_Wrapper.m_ship_yaw;
        public InputActionMap Get() { return m_Wrapper.m_ship; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipActions set) { return set.Get(); }
        public void SetCallbacks(IShipActions instance)
        {
            if (m_Wrapper.m_ShipActionsCallbackInterface != null)
            {
                @forward.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnForward;
                @forward.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnForward;
                @forward.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnForward;
                @yaw.started -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
                @yaw.performed -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
                @yaw.canceled -= m_Wrapper.m_ShipActionsCallbackInterface.OnYaw;
            }
            m_Wrapper.m_ShipActionsCallbackInterface = instance;
            if (instance != null)
            {
                @forward.started += instance.OnForward;
                @forward.performed += instance.OnForward;
                @forward.canceled += instance.OnForward;
                @yaw.started += instance.OnYaw;
                @yaw.performed += instance.OnYaw;
                @yaw.canceled += instance.OnYaw;
            }
        }
    }
    public ShipActions @ship => new ShipActions(this);

    // cannon
    private readonly InputActionMap m_cannon;
    private ICannonActions m_CannonActionsCallbackInterface;
    public struct CannonActions
    {
        private @PlayerActions m_Wrapper;
        public CannonActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_cannon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CannonActions set) { return set.Get(); }
        public void SetCallbacks(ICannonActions instance)
        {
            if (m_Wrapper.m_CannonActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_CannonActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public CannonActions @cannon => new CannonActions(this);

    // menu
    private readonly InputActionMap m_menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    public struct MenuActions
    {
        private @PlayerActions m_Wrapper;
        public MenuActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public MenuActions @menu => new MenuActions(this);
    private int m_PlaystationDualShockcontrollerSchemeIndex = -1;
    public InputControlScheme PlaystationDualShockcontrollerScheme
    {
        get
        {
            if (m_PlaystationDualShockcontrollerSchemeIndex == -1) m_PlaystationDualShockcontrollerSchemeIndex = asset.FindControlSchemeIndex("Playstation DualShock controller");
            return asset.controlSchemes[m_PlaystationDualShockcontrollerSchemeIndex];
        }
    }
    private int m_XboxcontrollerSchemeIndex = -1;
    public InputControlScheme XboxcontrollerScheme
    {
        get
        {
            if (m_XboxcontrollerSchemeIndex == -1) m_XboxcontrollerSchemeIndex = asset.FindControlSchemeIndex("Xbox controller");
            return asset.controlSchemes[m_XboxcontrollerSchemeIndex];
        }
    }
    private int m_NintendoProcontrollerSchemeIndex = -1;
    public InputControlScheme NintendoProcontrollerScheme
    {
        get
        {
            if (m_NintendoProcontrollerSchemeIndex == -1) m_NintendoProcontrollerSchemeIndex = asset.FindControlSchemeIndex("Nintendo Pro controller");
            return asset.controlSchemes[m_NintendoProcontrollerSchemeIndex];
        }
    }
    private int m_KeyboardTestingSchemeIndex = -1;
    public InputControlScheme KeyboardTestingScheme
    {
        get
        {
            if (m_KeyboardTestingSchemeIndex == -1) m_KeyboardTestingSchemeIndex = asset.FindControlSchemeIndex("Keyboard (Testing)");
            return asset.controlSchemes[m_KeyboardTestingSchemeIndex];
        }
    }
    public interface IShipActions
    {
        void OnForward(InputAction.CallbackContext context);
        void OnYaw(InputAction.CallbackContext context);
    }
    public interface ICannonActions
    {
    }
    public interface IMenuActions
    {
    }
}
