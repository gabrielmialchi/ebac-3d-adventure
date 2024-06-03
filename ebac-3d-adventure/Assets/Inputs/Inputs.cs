// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""30a10988-4763-4bd5-a808-f20a56dea96c"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""005153ff-7085-43ba-97da-fc5aeca2adb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GunShootLimit"",
                    ""type"": ""Button"",
                    ""id"": ""08d6c1df-8394-4cde-b4c8-394bee3ada69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GunAngle"",
                    ""type"": ""Button"",
                    ""id"": ""cd5cd719-4315-4003-9232-db6c621d98e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8be1fcbb-ce87-45ed-a598-7da195124e62"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02620e88-3da8-43b0-9c33-8192b22e7665"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GunShootLimit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de210755-6a41-467a-bfba-42305416a684"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GunAngle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_GunShootLimit = m_Gameplay.FindAction("GunShootLimit", throwIfNotFound: true);
        m_Gameplay_GunAngle = m_Gameplay.FindAction("GunAngle", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_GunShootLimit;
    private readonly InputAction m_Gameplay_GunAngle;
    public struct GameplayActions
    {
        private @Inputs m_Wrapper;
        public GameplayActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @GunShootLimit => m_Wrapper.m_Gameplay_GunShootLimit;
        public InputAction @GunAngle => m_Wrapper.m_Gameplay_GunAngle;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @GunShootLimit.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunShootLimit;
                @GunShootLimit.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunShootLimit;
                @GunShootLimit.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunShootLimit;
                @GunAngle.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunAngle;
                @GunAngle.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunAngle;
                @GunAngle.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGunAngle;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @GunShootLimit.started += instance.OnGunShootLimit;
                @GunShootLimit.performed += instance.OnGunShootLimit;
                @GunShootLimit.canceled += instance.OnGunShootLimit;
                @GunAngle.started += instance.OnGunAngle;
                @GunAngle.performed += instance.OnGunAngle;
                @GunAngle.canceled += instance.OnGunAngle;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnGunShootLimit(InputAction.CallbackContext context);
        void OnGunAngle(InputAction.CallbackContext context);
    }
}
