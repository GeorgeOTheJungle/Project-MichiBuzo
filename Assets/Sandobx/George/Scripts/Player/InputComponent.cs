using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public static InputComponent Instance { get; private set; }

    public delegate void MovementEvent(Vector2 value);
    public delegate void ShootEvent();

    public event MovementEvent movementTrigger;
    public event ShootEvent shotTrigger;

    private UserInput inputActions;
    private void Awake()
    {
        Instance = this;
        inputActions = new UserInput();
        inputActions.Enable();

        inputActions.Player.Action.performed += _ => shotTrigger?.Invoke();
    }

    private void Update()
    {
        //moveTrigger?.Invoke(playerInput.Player.Movement.ReadValue<Vector2>());
       movementTrigger?.Invoke(inputActions.Player.Movement.ReadValue<Vector2>());
        //inputActions.Player.Movement.performed += _ => movementTrigger?.Invoke(inputActions.Player.Movement.ReadValue<Vector2>());
    }
}
