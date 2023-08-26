using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState gameState;
    public int level = 0;
    [SerializeField] private bool goingDown = true;
    [SerializeField] private float divingPointWinRate = 0.25f;
    [SerializeField] private float risingPointWinRate = 0.5f;
    private float timer;
    public int storedScore;
    public int totalScore;

    [SerializeField] private GameObject mainMenuObjects;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject lostLevelButton;
    [SerializeField] private GameObject gameoverText;
    [Space(10)]
    [SerializeField] private GameObject interludeGO;
    [SerializeField] private TextMeshProUGUI interludeText;
    public GameObject goalGO;

    [Space(10)]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;

    public delegate void StopGame();
    public delegate void StartGame();

    public event StartGame onGameStart;
    public event StopGame onPlayerCollideTrigger;
    public event StopGame onLevelEnd;
    public event StopGame onOxygenEnd;


    private const string tScore = "Score: ";
    private const string tscoreText = "Total Score: ";
    private const string levelText = "Level - ";
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputComponent.Instance.startTrigger += ShowInterludeUI;
        InputComponent.Instance.startTrigger += RestartGame;
    }

    private void OnDisable()
    {
        InputComponent.Instance.startTrigger -= ShowInterludeUI;
        InputComponent.Instance.startTrigger -= RestartGame;
    }

    public void OnPlayerHit()
    {
        onPlayerCollideTrigger?.Invoke();
        OnLevelLost();
    }

    public void OnOxygenEnd()
    {
        onOxygenEnd?.Invoke();
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

    public void OnPointsGather(int value,bool isChest)
    {
        storedScore += value;
        UpdateUI();
        if (isChest == false) return;
        goingDown = false;
        goalGO.SetActive(true);
    }

    public void OnGameStart()
    {
        goingDown = true;
        SetGameState(GameState.moving);
        UpdateUI();
        onGameStart?.Invoke();
        mainMenuObjects.SetActive(false);
    }

    private void UpdateUI()
    {
        scoreText.SetText(tScore + storedScore);
    }

    public void OnLevelFinished()
    {
        onLevelEnd?.Invoke();
        //totalScore += storedScore;
        SetGameState(GameState.finished);
        StartCoroutine(ScoreAnimation(false));
    }

    public void OnLevelLost()
    {
        onLevelEnd?.Invoke();
        SetGameState(GameState.finished);
        StartCoroutine(ScoreAnimation(true));
    }

    private bool canRestart = false;

    private IEnumerator ScoreAnimation(bool lost)
    {
        // Player Exit Animation
        AudioManager.Instance.StopSong();
        yield return new WaitForSeconds(0.5f);
        scoreUI.SetActive(true);
        gameoverText.SetActive(lost);
        yield return new WaitForSeconds(0.75f);
        while(storedScore > 0)
        {
            storedScore -= 3;
            totalScore += 3;
            UpdateUI();
            if (storedScore < 0) storedScore = 0;
            totalScoreText.SetText(tscoreText + totalScore.ToString());
            yield return new WaitForEndOfFrame();
        }
        storedScore = 0;
        yield return new WaitForSeconds(0.5f);
        if (lost) {
            canRestart = true;
            lostLevelButton.SetActive(true);
        }  else
        {
            inGame = false;
            nextLevelButton.SetActive(true);
        }
 
    }

    public void SetGameState(GameState newState)
    {
        gameState = newState;
    }

    public void RestartGame()
    {
        if (canRestart == false) return;
        SceneManager.LoadScene(0);
    }

    public void ShowInterludeUI()
    {
        if (canRestart) return;
        if (inGame) return;
        inGame = true;
        gameMenu.SetActive(true);
        mainMenu.SetActive(false);

        level++;
        lostLevelButton.SetActive(false);
        nextLevelButton.SetActive(false);
        gameoverText.SetActive(false);
        StartCoroutine(InterludeAnimation());
    }

    private bool inGame = false;

    private IEnumerator InterludeAnimation()
    {

        AudioManager.Instance.StopSong();
        LevelManager.Instance.CleanLevelList();
        scoreUI.SetActive(false);
        interludeGO.SetActive(true);
        interludeText.SetText(levelText + level.ToString());
        interludeText.text = levelText + level.ToString();
        LevelManager.Instance.HandleLevelGeneration();
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.PlaySong(false);
        interludeGO.SetActive(false);
        LevelManager.Instance.StartGoingDown();
        OnGameStart();
    }

    public enum GameState { paused,moving,waiting,finished}
}
