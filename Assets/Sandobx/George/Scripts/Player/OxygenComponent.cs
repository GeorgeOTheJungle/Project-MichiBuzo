using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OxygenComponent : MonoBehaviour
{
    public static OxygenComponent Instance { get; private set; }

    [Header("Oxygen Component: "), Space(10)]
    [SerializeField] private bool infiniteOxygen;
    [SerializeField] private float oxygenMax;
    [SerializeField] private float currentOxygen;

    [SerializeField] private Image oxygenBar;
    [SerializeField] private Animator playerAnimator;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentOxygen = oxygenMax;
        GameManager.Instance.onGameStart += ResetOxygen;
    }

    private void OnEnable()
    {
        if (GameManager.Instance) GameManager.Instance.onGameStart += ResetOxygen;
    }

    private void OnDisable()
    {
        if (GameManager.Instance) GameManager.Instance.onGameStart -= ResetOxygen;
    }

    private void Update()
    {
        if (infiniteOxygen) return;
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.gameState == GameManager.GameState.moving || GameManager.Instance.gameState == GameManager.GameState.waiting)
        {
            if (currentOxygen > 0f) currentOxygen -= CustomTime.DeltaTime;
            else
            {
                // Death here
       
                playerAnimator.SetTrigger("death");
                GameManager.Instance.OnOxygenEnd();
                currentOxygen = 0f;
            }
            UpdateUI();
        }
    }

    public void OnOxygenCollection(float value)
    {
        currentOxygen += value;
        if(currentOxygen > oxygenMax) currentOxygen = oxygenMax;
        UpdateUI();
    }

    private void UpdateUI()
    {
        oxygenBar.fillAmount = currentOxygen / oxygenMax;
    }

    private void ResetOxygen()
    {
        currentOxygen = oxygenMax;
        UpdateUI();
    }
}
