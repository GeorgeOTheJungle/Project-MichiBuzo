using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Movement manager"), Space(10)]
    [SerializeField] private bool canMove = true;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    [Header("Constrains"), Space(10)]
    [SerializeField] private float rangeConstrain = 3.0f;
    [SerializeField] private float verticalRangeConstrain;
    
    Vector2 moveDir;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector2.zero, new Vector2(rangeConstrain * 2f, 15f));
    }

    private void OnEnable()
    {
        if (InputComponent.Instance) InputComponent.Instance.movementTrigger += HandleMovement;
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger += StopMoving;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd += StopMoving;
    }

    private void OnDisable()
    {
        if(InputComponent.Instance) InputComponent.Instance.movementTrigger -= HandleMovement;
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger -= StopMoving;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd -= StopMoving;
    }

    private void Start()
    {
        InputComponent.Instance.movementTrigger += HandleMovement;
        GameManager.Instance.onPlayerCollideTrigger += StopMoving;
        GameManager.Instance.onLevelEnd += StopMoving;
    }
    private void HandleMovement(Vector2 direction)
    {
        if (!canMove) return;
        moveDir = direction;
        float hSpeed = horizontalSpeed;
        float vSpeed = verticalSpeed;
        if (moveDir.x > 0.1f)
        {
            if (transform.position.x < rangeConstrain) hSpeed = horizontalSpeed;
            else hSpeed = 0f;
  
        }
        else if (moveDir.x < -0.1f)
        {
            if (transform.position.x > -rangeConstrain) hSpeed = horizontalSpeed;
            else hSpeed = 0f;
            //if (transform.position.x > -rangeConstrain)
            //    transform.Translate(moveDir * horizontalSpeed * CustomTime.DeltaTime);
        }

        if(moveDir.y > 0.0f)
        {
            if(transform.position.y < verticalRangeConstrain) vSpeed = verticalSpeed;
            else vSpeed = 0f;
        }

        transform.Translate(moveDir * new Vector2(hSpeed, vSpeed) * CustomTime.DeltaTime);
    }

    public void StopMoving()
    {
        canMove = false;
        Debug.Log("I stopped!!!");
    }
}
