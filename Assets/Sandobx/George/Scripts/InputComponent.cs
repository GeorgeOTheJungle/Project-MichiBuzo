using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputComponent : MonoBehaviour
{
    public static InputComponent Instance { get; private set; }

    public delegate void MovementEvent(Vector2 value);

    public event MovementEvent movementTrigger;

    private UserInput inputActions;
    private void Awake()
    {
        Instance = this;
        inputActions = new UserInput();
        inputActions.Enable();
    }

    private void Update()
    {
        inputActions.Player.Movement.performed += _ => movementTrigger?.Invoke(inputActions.Player.Movement.ReadValue<Vector2>());
    }
}
