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

    private Vector2 originalPosition;
    Vector2 moveDir;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform visual;
    [SerializeField] private ParticleSystem oxygenParticles;
    [SerializeField] private Transform startingPoint;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector2.zero, new Vector2(rangeConstrain * 2f, verticalRangeConstrain * 2f));
    }

    private void OnEnable()
    {
        if (InputComponent.Instance) InputComponent.Instance.movementTrigger += HandleMovement;
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger += StopMoving;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd += StopMoving;
        if (GameManager.Instance) GameManager.Instance.onOxygenEnd += StopMoving;
        if (GameManager.Instance) GameManager.Instance.onGameStart += PlacePlayerOnPoint;
    }

    private void OnDisable()
    {
        if(InputComponent.Instance) InputComponent.Instance.movementTrigger -= HandleMovement;
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger -= StopMoving;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd -= StopMoving;
        if (GameManager.Instance) GameManager.Instance.onOxygenEnd -= StopMoving;
        if (GameManager.Instance) GameManager.Instance.onGameStart -= PlacePlayerOnPoint;
    }

    private void Start()
    {
        InputComponent.Instance.movementTrigger += HandleMovement;
        GameManager.Instance.onPlayerCollideTrigger += StopMoving;
        GameManager.Instance.onLevelEnd += StopMoving;
        GameManager.Instance.onOxygenEnd += StopMoving;
        GameManager.Instance.onGameStart += PlacePlayerOnPoint;

        originalPosition = startingPoint.position;
    }

    private void Update()
    {
        switch (LevelManager.Instance.GetDirection())
        {
            case LevelManager.MovementDirection.up:
                playerAnimator.SetFloat("dir", 1f);
                break;
            case LevelManager.MovementDirection.none:
                playerAnimator.SetFloat("dir", 0f);
                break;
            case LevelManager.MovementDirection.down:
                playerAnimator.SetFloat("dir", -1f);
                break;
        }
    }
    private float bottom;
    private void HandleMovement(Vector2 direction)
    {
        if (!canMove) return;
        if (GameManager.Instance.gameState == GameManager.GameState.finished || GameManager.Instance.gameState == GameManager.GameState.paused) return;
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
        } else if(moveDir .y < -0.1f)
        {
            bottom = LevelManager.Instance.GetMaxHeight();
            if (transform.position.y > -verticalRangeConstrain) vSpeed = verticalSpeed;
            else vSpeed = 0f;
        }

        transform.Translate(moveDir * new Vector2(hSpeed, vSpeed) * CustomTime.DeltaTime);

        if (moveDir.x > 0.0f && isLeft) Flip();
        else if (moveDir.x < 0.0f && !isLeft) Flip();

        playerAnimator.SetBool("isMoving", direction != Vector2.zero);
        playerAnimator.SetFloat("moveX", direction.x);
        playerAnimator.SetFloat("moveY", direction.y);
    }

    private void PlacePlayerOnPoint()
    {
        transform.position = originalPosition;
        canMove = true;
        playerAnimator.SetTrigger("gameStart");
        oxygenParticles.Play();
    }
    public void StopMoving()
    {
        canMove = false;
        oxygenParticles.Stop();
    }

    private bool isLeft = false;
    private void Flip()
    {
        isLeft = !isLeft;
        if (isLeft)
        {
            visual.localScale = new Vector2(-1, 1f);
        } else
        {
            visual.localScale = new Vector2(1f, 1f);
        }
    }
}
