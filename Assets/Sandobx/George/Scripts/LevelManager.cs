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
    [SerializeField] private int baseFillLevels;
    [SerializeField] private float levelSeparation;
    [Space(10)]
    [SerializeField] private GameObject[] posibleFillLevels;
    [SerializeField] private GameObject[] posibleEndingLevels;

    [SerializeField] private List<GameObject> currentLevels = new List<GameObject>();
    private float originalHeight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalHeight = transform.position.y;
        GameManager.Instance.onPlayerCollideTrigger += PauseMovement;
        GameManager.Instance.onOxygenEnd += StopMovement;
        GameManager.Instance.onLevelEnd += StopMovement;

    }

    private void OnEnable()
    {
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger += PauseMovement;
        if (GameManager.Instance) GameManager.Instance.onOxygenEnd += StopMovement;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd += StopMovement;
    }

    private void OnDisable()
    {
        if (GameManager.Instance) GameManager.Instance.onPlayerCollideTrigger -= PauseMovement;
        if (GameManager.Instance) GameManager.Instance.onOxygenEnd -= StopMovement;
        if (GameManager.Instance) GameManager.Instance.onLevelEnd -= StopMovement;
    }

    private void Update()
    {
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
            else PauseMovement();
        } else
        {
            if (transform.position.y > originalHeight)
            {
                transform.Translate(Vector2.down * verticalSpeed * CustomTime.DeltaTime);
            }
            else PauseMovement();
        }

    }
    int totalGeneration;
    public void HandleLevelGeneration()
    {
        //  Instantiate(startingLevel, transform.position, Quaternion.identity,transform);
        totalGeneration = baseFillLevels + GameManager.Instance.level;
        maxHeight = (totalGeneration + 1) * 10;
        Vector2 fragmentPosition = transform.position;
        fragmentPosition.y -= levelSeparation;
        for (int i = 0;i < totalGeneration; i++)
        {
            GameObject level = Instantiate(posibleFillLevels[Random.Range(0, posibleFillLevels.Length)],
                fragmentPosition, Quaternion.identity, transform);
            currentLevels.Add(level);
            fragmentPosition.y -= levelSeparation;
        }
        GameObject final = Instantiate(posibleEndingLevels[Random.Range(0, posibleEndingLevels.Length)],
            fragmentPosition, Quaternion.identity, transform);
        currentLevels.Add(final);

    }

    public void StartGoingDown()
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
    
    private void StopMovement()
    {
        moving = false;
        direction = MovementDirection.none;
        GameManager.Instance.SetGameState(GameManager.GameState.paused);
    }

    private void PauseMovement()
    {
        moving = false;
        direction = MovementDirection.none;
        GameManager.Instance.SetGameState(GameManager.GameState.waiting);
    }

    public void CleanLevelList()
    {
        if (currentLevels.Count == 0) return;
        foreach(GameObject go in currentLevels)
        {
            Destroy(go);
        }
        currentLevels.Clear();
    }

    public MovementDirection GetDirection()
    {
        return direction;
    }
    public enum MovementDirection { up, down,none}
}
