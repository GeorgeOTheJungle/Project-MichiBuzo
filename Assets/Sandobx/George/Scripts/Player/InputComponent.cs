using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
public class InputComponent : MonoBehaviour
{
    public static InputComponent Instance { get; private set; }

    public delegate void MovementEvent(Vector2 value);
    public delegate void ShootEvent();
    public delegate void StartEvent();

    public event MovementEvent movementTrigger;
    public event ShootEvent shotTrigger;
    public event StartEvent startTrigger;

    private UserInput inputActions;

    private bool gameStarted;
    private void Awake()
    {
        Instance = this;
        inputActions = new UserInput();
        inputActions.Enable();

        inputActions.Player.Action.performed += _ => shotTrigger?.Invoke();
        inputActions.Player.Action.performed += _ => startTrigger?.Invoke();

    }

    //private void OnEnable()
    //{
    //    startTrigger += OnGameStart;
    //}

    //private void OnDisable()
    //{
    //    startTrigger -= OnGameStart;
    //}

    private void Update()
    {
        //moveTrigger?.Invoke(playerInput.Player.Movement.ReadValue<Vector2>());
       movementTrigger?.Invoke(inputActions.Player.Movement.ReadValue<Vector2>());

       // if (gameStarted == true) return;
        //InputSystem.onAnyButtonPress.CallOnce(ctrl => startTrigger?.Invoke());
        //inputActions.Player.Movement.performed += _ => movementTrigger?.Invoke(inputActions.Player.Movement.ReadValue<Vector2>());
    }

    //private void OnGameStart()
    //{
    //    gameStarted = true;
    //}

    //public void OnGameRestart()
    //{
    //    gameStarted = false;
    //}
}
