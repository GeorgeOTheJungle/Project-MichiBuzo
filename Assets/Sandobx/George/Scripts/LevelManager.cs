using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [Header("Level manager"), Space(10)]
    [SerializeField] private bool moving = false;
    [SerializeField] private MovementDirection direction;
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float maxHeight;

    [Header("Posible levels: "), Space(10)]
    [SerializeField] private int totalFillLevels;
    [SerializeField] private float levelSeparation;
    [Space(10)]
    [SerializeField] private GameObject startingLevel;
    [SerializeField] private GameObject[] posibleFillLevels;
    [SerializeField] private GameObject[] posibleEndingLevels;
    private float originalHeight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalHeight = transform.position.y;
        GameManager.Instance.onPlayerCollideTrigger += StopMoving;
        maxHeight = (totalFillLevels + 1) * 10;
    }

    private void OnEnable()
    {
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger += StopMoving;
    }

    private void OnDisable()
    {
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger -= StopMoving;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartGoingDown();
            GameManager.Instance.OnGameStart();
        } else if (Input.GetKeyDown(KeyCode.U))
        {
            StartGoingUp();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            HandleLevelGeneration();
        }

        if (moving) HandleLevelMovement();
    }

    private void HandleLevelMovement()
    {
        if(direction == MovementDirection.down)
        {
            if (transform.position.y < maxHeight)
            {
                transform.Translate(Vector2.up * verticalSpeed * CustomTime.DeltaTime);
            }
            else StopMoving();
        } else
        {
            if (transform.position.y > originalHeight)
            {
                transform.Translate(Vector2.down * verticalSpeed * CustomTime.DeltaTime);
            }
            else StopMoving();
        }

    }

    private void HandleLevelGeneration()
    {
        Instantiate(startingLevel, transform.position, Quaternion.identity,transform);
        Vector2 fragmentPosition = transform.position;
        fragmentPosition.y -= levelSeparation;
        for (int i = 0;i < totalFillLevels; i++)
        {
            Instantiate(posibleFillLevels[Random.Range(0, posibleFillLevels.Length)], fragmentPosition, Quaternion.identity, transform);
            fragmentPosition.y -= levelSeparation;
        }
        Instantiate(posibleEndingLevels[Random.Range(0, posibleEndingLevels.Length)], fragmentPosition, Quaternion.identity, transform);

    }

    private void StartGoingDown()
    {
        direction = MovementDirection.down;
        GameManager.Instance.SetGameState(GameManager.GameState.moving);
        moving = true;
    }

    public void StartGoingUp()
    {
        direction = MovementDirection.up;
        GameManager.Instance.SetGameState(GameManager.GameState.moving);
        moving = true;
    }

    private void StopMoving()
    {
        moving = false;
        GameManager.Instance.SetGameState(GameManager.GameState.waiting);
    }

    private enum MovementDirection { up, down}
}
