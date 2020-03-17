// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/controlos ps4.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controlosps4 : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controlosps4()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""controlos ps4"",
    ""maps"": [
        {
            ""name"": ""Player controls PS4"",
            ""id"": ""97a4ae8d-684b-4bf5-a02f-51d8fef26266"",
            ""actions"": [
                {
                    ""name"": ""Mover"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a9bd3040-c69b-4447-92ae-6c79a98f7ac7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9f9d38ba-9caa-4f71-b1e5-1937f5c859f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""68a5e67b-393b-4861-88a7-38f3f651c8d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Olhar"",
                    ""type"": ""PassThrough"",
                    ""id"": ""08e25d04-2dee-4161-97d3-a4ad27685345"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a420097f-3e2c-4a40-88de-4a2738ca928c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""cd3fddea-59ee-4dec-a3bb-599dcd536054"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""32f2276c-81f6-44ab-95a3-a44cf8ec8da3"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": ""Press(pressPoint=0.9,behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""377c594e-4f52-49c1-9a57-139531bec868"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71afc97e-f93b-4a33-8a1f-8b81a7f44194"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23f608de-a7d5-4462-91ec-8bf2cab798d7"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0cdb213d-dc55-4f92-abef-8632f08e9644"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc657fca-330b-447a-b4e2-2d6b8545bea9"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bc339f1-2d64-4099-ac60-7fadace8df43"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Mover"",
                    ""id"": ""df098f70-e6c1-469d-824a-5acf05560dfb"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Hold"",
                    ""processors"": ""ScaleVector2(x=4,y=4)"",
                    ""groups"": """",
                    ""action"": ""Mover"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c4a8fd21-a142-4173-8b9a-ff69ad761e55"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c69918e5-6ce9-49ab-bdfe-6207668789e4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6537f411-aad7-4360-bb2d-ad6b9bcd4a1e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""edc0c492-5ed5-4b48-bc0d-c7ccda604b98"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ed1885a8-5e67-4a22-9dfb-48670224c8d7"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Mover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""455af26b-6d62-4c08-a001-0d3d12526bc5"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Olhar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba1fffdb-b4e6-40c3-9adc-736e31c617a5"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.2,y=0.2)"",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Olhar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ca48cea-5b82-4fbe-84b3-89c54fba20bb"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Olhar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bac733c-6962-4457-a6c0-7b342f819fca"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""942465ce-f524-4207-a289-4adb42287021"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c4ddc75-f924-405d-a1e8-dc1da3679b71"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4091d87f-252d-42cc-a562-db1ffd387f34"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f3c2123-d893-4023-8999-ca2715ef0ac5"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""teclado_debug"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e49db79-6069-4b99-9360-54caf4c0aa10"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PS4"",
            ""bindingGroup"": ""PS4"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""teclado_debug"",
            ""bindingGroup"": ""teclado_debug"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player controls PS4
        m_PlayercontrolsPS4 = asset.FindActionMap("Player controls PS4", throwIfNotFound: true);
        m_PlayercontrolsPS4_Mover = m_PlayercontrolsPS4.FindAction("Mover", throwIfNotFound: true);
        m_PlayercontrolsPS4_Shoot = m_PlayercontrolsPS4.FindAction("Shoot", throwIfNotFound: true);
        m_PlayercontrolsPS4_Reload = m_PlayercontrolsPS4.FindAction("Reload", throwIfNotFound: true);
        m_PlayercontrolsPS4_Olhar = m_PlayercontrolsPS4.FindAction("Olhar", throwIfNotFound: true);
        m_PlayercontrolsPS4_Jump = m_PlayercontrolsPS4.FindAction("Jump", throwIfNotFound: true);
        m_PlayercontrolsPS4_Run = m_PlayercontrolsPS4.FindAction("Run", throwIfNotFound: true);
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

    // Player controls PS4
    private readonly InputActionMap m_PlayercontrolsPS4;
    private IPlayercontrolsPS4Actions m_PlayercontrolsPS4ActionsCallbackInterface;
    private readonly InputAction m_PlayercontrolsPS4_Mover;
    private readonly InputAction m_PlayercontrolsPS4_Shoot;
    private readonly InputAction m_PlayercontrolsPS4_Reload;
    private readonly InputAction m_PlayercontrolsPS4_Olhar;
    private readonly InputAction m_PlayercontrolsPS4_Jump;
    private readonly InputAction m_PlayercontrolsPS4_Run;
    public struct PlayercontrolsPS4Actions
    {
        private @Controlosps4 m_Wrapper;
        public PlayercontrolsPS4Actions(@Controlosps4 wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mover => m_Wrapper.m_PlayercontrolsPS4_Mover;
        public InputAction @Shoot => m_Wrapper.m_PlayercontrolsPS4_Shoot;
        public InputAction @Reload => m_Wrapper.m_PlayercontrolsPS4_Reload;
        public InputAction @Olhar => m_Wrapper.m_PlayercontrolsPS4_Olhar;
        public InputAction @Jump => m_Wrapper.m_PlayercontrolsPS4_Jump;
        public InputAction @Run => m_Wrapper.m_PlayercontrolsPS4_Run;
        public InputActionMap Get() { return m_Wrapper.m_PlayercontrolsPS4; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayercontrolsPS4Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayercontrolsPS4Actions instance)
        {
            if (m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface != null)
            {
                @Mover.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnMover;
                @Mover.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnMover;
                @Mover.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnMover;
                @Shoot.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnShoot;
                @Reload.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnReload;
                @Olhar.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnOlhar;
                @Olhar.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnOlhar;
                @Olhar.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnOlhar;
                @Jump.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnJump;
                @Run.started -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_PlayercontrolsPS4ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mover.started += instance.OnMover;
                @Mover.performed += instance.OnMover;
                @Mover.canceled += instance.OnMover;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Olhar.started += instance.OnOlhar;
                @Olhar.performed += instance.OnOlhar;
                @Olhar.canceled += instance.OnOlhar;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public PlayercontrolsPS4Actions @PlayercontrolsPS4 => new PlayercontrolsPS4Actions(this);
    private int m_PS4SchemeIndex = -1;
    public InputControlScheme PS4Scheme
    {
        get
        {
            if (m_PS4SchemeIndex == -1) m_PS4SchemeIndex = asset.FindControlSchemeIndex("PS4");
            return asset.controlSchemes[m_PS4SchemeIndex];
        }
    }
    private int m_teclado_debugSchemeIndex = -1;
    public InputControlScheme teclado_debugScheme
    {
        get
        {
            if (m_teclado_debugSchemeIndex == -1) m_teclado_debugSchemeIndex = asset.FindControlSchemeIndex("teclado_debug");
            return asset.controlSchemes[m_teclado_debugSchemeIndex];
        }
    }
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    public interface IPlayercontrolsPS4Actions
    {
        void OnMover(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnOlhar(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
    }
}
