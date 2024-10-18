//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Input/Player Controls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""5a324d24-0d08-4063-a4f0-6732238250a9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""1ab5992c-130f-48ba-a506-384b65e3b0fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""bf5a81be-92b3-4b12-9a2f-814f383acc2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""c46ebd0d-daa7-4477-8dc4-034db9c32d87"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""5ce6f3d3-dd1d-4cf3-bcb0-e4e7c665c703"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9065b6ae-4292-4218-84c9-eaccafbc8715"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""49d3cf37-2981-44b5-a6f2-83afd4474bf8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8b7e3dce-5c19-47a7-a2c0-568b1cbe0122"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""13aea260-8889-4cf2-bbad-e0e4c66d2c0c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f031d419-2987-4bb4-9909-5edd162c4e5a"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cf86c079-af42-43a6-b9ab-a8f6ab89bb13"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fd9d735-e313-41d0-abbe-9f94362ba1b2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51b72a27-d718-4fe2-ad2f-2cb40ded6069"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CoffeGame"",
            ""id"": ""13890fbf-db86-409f-83d4-9919c16fa9c0"",
            ""actions"": [
                {
                    ""name"": ""FirstPress"",
                    ""type"": ""Button"",
                    ""id"": ""633891cd-e315-4881-b41e-21daac4bc66a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondPress"",
                    ""type"": ""Button"",
                    ""id"": ""3916800e-f6ab-4e2f-b991-3f0162218424"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThirdPress"",
                    ""type"": ""Button"",
                    ""id"": ""bb70e64e-7b9d-4ff4-82f5-e8d1e717e6ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FourthPress"",
                    ""type"": ""Button"",
                    ""id"": ""58ec7f09-a49c-49a1-b356-cbc2c67144c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""EyesClose"",
                    ""type"": ""Button"",
                    ""id"": ""bfde3484-7b88-43ac-9ce8-9aa9be329d34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BallMovement"",
                    ""type"": ""Value"",
                    ""id"": ""e305d9fd-04ea-4625-841c-f0da3b82dd06"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""13db46ca-f060-43b7-84ad-a4586f32d389"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""FirstPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba7df9ac-8a90-44be-ab9b-c0472eeaabdb"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""SecondPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""959a3290-3608-4ee5-aef1-68f6919097c9"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""ThirdPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d213fe5-0cbd-4f03-be23-808f8104b230"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""FourthPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db37f811-f991-45fd-a0ba-f454002404f2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""EyesClose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up/Down/Right/Left"",
                    ""id"": ""2a4216aa-004c-43d1-9bbc-381b6bec6a8c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BallMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""89ee84c1-1622-456c-aadf-ffbb1106e46e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BallMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""66323f63-18a0-4d6a-ae6b-a27430058031"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BallMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""286af4e9-9cb5-4bf5-b6c1-6bda970984d3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BallMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f1ea4d0f-027b-4061-a68f-225c9b09726c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BallMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""GeneralNavigation"",
            ""id"": ""1325da1c-499b-4c96-9a18-71391ac16bb0"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""87573a2c-7040-464d-a389-3894a5000618"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b56e1e10-ac23-4c50-8135-760264787d51"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": []
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_Interact = m_Movement.FindAction("Interact", throwIfNotFound: true);
        m_Movement_MouseClick = m_Movement.FindAction("MouseClick", throwIfNotFound: true);
        // CoffeGame
        m_CoffeGame = asset.FindActionMap("CoffeGame", throwIfNotFound: true);
        m_CoffeGame_FirstPress = m_CoffeGame.FindAction("FirstPress", throwIfNotFound: true);
        m_CoffeGame_SecondPress = m_CoffeGame.FindAction("SecondPress", throwIfNotFound: true);
        m_CoffeGame_ThirdPress = m_CoffeGame.FindAction("ThirdPress", throwIfNotFound: true);
        m_CoffeGame_FourthPress = m_CoffeGame.FindAction("FourthPress", throwIfNotFound: true);
        m_CoffeGame_EyesClose = m_CoffeGame.FindAction("EyesClose", throwIfNotFound: true);
        m_CoffeGame_BallMovement = m_CoffeGame.FindAction("BallMovement", throwIfNotFound: true);
        // GeneralNavigation
        m_GeneralNavigation = asset.FindActionMap("GeneralNavigation", throwIfNotFound: true);
        m_GeneralNavigation_PauseGame = m_GeneralNavigation.FindAction("PauseGame", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private List<IMovementActions> m_MovementActionsCallbackInterfaces = new List<IMovementActions>();
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_Interact;
    private readonly InputAction m_Movement_MouseClick;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @Interact => m_Wrapper.m_Movement_Interact;
        public InputAction @MouseClick => m_Wrapper.m_Movement_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void AddCallbacks(IMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_MovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MovementActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @MouseClick.started += instance.OnMouseClick;
            @MouseClick.performed += instance.OnMouseClick;
            @MouseClick.canceled += instance.OnMouseClick;
        }

        private void UnregisterCallbacks(IMovementActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @MouseClick.started -= instance.OnMouseClick;
            @MouseClick.performed -= instance.OnMouseClick;
            @MouseClick.canceled -= instance.OnMouseClick;
        }

        public void RemoveCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_MovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // CoffeGame
    private readonly InputActionMap m_CoffeGame;
    private List<ICoffeGameActions> m_CoffeGameActionsCallbackInterfaces = new List<ICoffeGameActions>();
    private readonly InputAction m_CoffeGame_FirstPress;
    private readonly InputAction m_CoffeGame_SecondPress;
    private readonly InputAction m_CoffeGame_ThirdPress;
    private readonly InputAction m_CoffeGame_FourthPress;
    private readonly InputAction m_CoffeGame_EyesClose;
    private readonly InputAction m_CoffeGame_BallMovement;
    public struct CoffeGameActions
    {
        private @PlayerControls m_Wrapper;
        public CoffeGameActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FirstPress => m_Wrapper.m_CoffeGame_FirstPress;
        public InputAction @SecondPress => m_Wrapper.m_CoffeGame_SecondPress;
        public InputAction @ThirdPress => m_Wrapper.m_CoffeGame_ThirdPress;
        public InputAction @FourthPress => m_Wrapper.m_CoffeGame_FourthPress;
        public InputAction @EyesClose => m_Wrapper.m_CoffeGame_EyesClose;
        public InputAction @BallMovement => m_Wrapper.m_CoffeGame_BallMovement;
        public InputActionMap Get() { return m_Wrapper.m_CoffeGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CoffeGameActions set) { return set.Get(); }
        public void AddCallbacks(ICoffeGameActions instance)
        {
            if (instance == null || m_Wrapper.m_CoffeGameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CoffeGameActionsCallbackInterfaces.Add(instance);
            @FirstPress.started += instance.OnFirstPress;
            @FirstPress.performed += instance.OnFirstPress;
            @FirstPress.canceled += instance.OnFirstPress;
            @SecondPress.started += instance.OnSecondPress;
            @SecondPress.performed += instance.OnSecondPress;
            @SecondPress.canceled += instance.OnSecondPress;
            @ThirdPress.started += instance.OnThirdPress;
            @ThirdPress.performed += instance.OnThirdPress;
            @ThirdPress.canceled += instance.OnThirdPress;
            @FourthPress.started += instance.OnFourthPress;
            @FourthPress.performed += instance.OnFourthPress;
            @FourthPress.canceled += instance.OnFourthPress;
            @EyesClose.started += instance.OnEyesClose;
            @EyesClose.performed += instance.OnEyesClose;
            @EyesClose.canceled += instance.OnEyesClose;
            @BallMovement.started += instance.OnBallMovement;
            @BallMovement.performed += instance.OnBallMovement;
            @BallMovement.canceled += instance.OnBallMovement;
        }

        private void UnregisterCallbacks(ICoffeGameActions instance)
        {
            @FirstPress.started -= instance.OnFirstPress;
            @FirstPress.performed -= instance.OnFirstPress;
            @FirstPress.canceled -= instance.OnFirstPress;
            @SecondPress.started -= instance.OnSecondPress;
            @SecondPress.performed -= instance.OnSecondPress;
            @SecondPress.canceled -= instance.OnSecondPress;
            @ThirdPress.started -= instance.OnThirdPress;
            @ThirdPress.performed -= instance.OnThirdPress;
            @ThirdPress.canceled -= instance.OnThirdPress;
            @FourthPress.started -= instance.OnFourthPress;
            @FourthPress.performed -= instance.OnFourthPress;
            @FourthPress.canceled -= instance.OnFourthPress;
            @EyesClose.started -= instance.OnEyesClose;
            @EyesClose.performed -= instance.OnEyesClose;
            @EyesClose.canceled -= instance.OnEyesClose;
            @BallMovement.started -= instance.OnBallMovement;
            @BallMovement.performed -= instance.OnBallMovement;
            @BallMovement.canceled -= instance.OnBallMovement;
        }

        public void RemoveCallbacks(ICoffeGameActions instance)
        {
            if (m_Wrapper.m_CoffeGameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICoffeGameActions instance)
        {
            foreach (var item in m_Wrapper.m_CoffeGameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CoffeGameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CoffeGameActions @CoffeGame => new CoffeGameActions(this);

    // GeneralNavigation
    private readonly InputActionMap m_GeneralNavigation;
    private List<IGeneralNavigationActions> m_GeneralNavigationActionsCallbackInterfaces = new List<IGeneralNavigationActions>();
    private readonly InputAction m_GeneralNavigation_PauseGame;
    public struct GeneralNavigationActions
    {
        private @PlayerControls m_Wrapper;
        public GeneralNavigationActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseGame => m_Wrapper.m_GeneralNavigation_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_GeneralNavigation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralNavigationActions set) { return set.Get(); }
        public void AddCallbacks(IGeneralNavigationActions instance)
        {
            if (instance == null || m_Wrapper.m_GeneralNavigationActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GeneralNavigationActionsCallbackInterfaces.Add(instance);
            @PauseGame.started += instance.OnPauseGame;
            @PauseGame.performed += instance.OnPauseGame;
            @PauseGame.canceled += instance.OnPauseGame;
        }

        private void UnregisterCallbacks(IGeneralNavigationActions instance)
        {
            @PauseGame.started -= instance.OnPauseGame;
            @PauseGame.performed -= instance.OnPauseGame;
            @PauseGame.canceled -= instance.OnPauseGame;
        }

        public void RemoveCallbacks(IGeneralNavigationActions instance)
        {
            if (m_Wrapper.m_GeneralNavigationActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGeneralNavigationActions instance)
        {
            foreach (var item in m_Wrapper.m_GeneralNavigationActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GeneralNavigationActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GeneralNavigationActions @GeneralNavigation => new GeneralNavigationActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
    }
    public interface ICoffeGameActions
    {
        void OnFirstPress(InputAction.CallbackContext context);
        void OnSecondPress(InputAction.CallbackContext context);
        void OnThirdPress(InputAction.CallbackContext context);
        void OnFourthPress(InputAction.CallbackContext context);
        void OnEyesClose(InputAction.CallbackContext context);
        void OnBallMovement(InputAction.CallbackContext context);
    }
    public interface IGeneralNavigationActions
    {
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
