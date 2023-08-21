using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;
    [SerializeField] private bool goingDown = true;
    [SerializeField] private float divingPointWinRate = 0.25f;
    [SerializeField] private float risingPointWinRate = 0.5f;
    private float timer;
    public int storedScore;
    public int totalScore;

    [SerializeField] private TextMeshPro scoreText;
    public GameObject goalGO;
    public delegate void StopGame();

    public event StopGame onPlayerCollideTrigger;
    public event StopGame onLevelEnd;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPlayerHit()
    {
        onPlayerCollideTrigger?.Invoke();
    }

    private void Update()
    {
        if(gameState == GameState.moving)
        {
            if (timer > 0f)
            {
                timer -= CustomTime.DeltaTime;
            }
            else
            {
                GainPoints();
            }
        }
    }

    private void GainPoints()
    {
        timer = goingDown ? divingPointWinRate : risingPointWinRate;
        storedScore += Mathf.CeilToInt(0.01f * CustomTime.DeltaTime);
        UpdateUI();
    }

    public void OnCoinGather()
    {
        storedScore += 100;
        UpdateUI();
    }

    public void OnGameStart()
    {
        goingDown = true;
        UpdateUI();
    }

    public void OnTreasureGather(int value)
    {
        storedScore += value;
        goingDown = false;
        goalGO.SetActive(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + storedScore;
    }

    public void OnLevelFinished()
    {
        onLevelEnd?.Invoke();
        totalScore += storedScore;
        Debug.Log("Level finished, well done!!!");
    }

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }

    public enum GameState { paused,moving,waiting}
}
