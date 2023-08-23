using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, ICollectable
{
    [SerializeField] private int pointsValue = 100;
    public void OnCollection()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnPointsGather(pointsValue, true);
        LevelManager.Instance.StartGoingUp();
    }
}
