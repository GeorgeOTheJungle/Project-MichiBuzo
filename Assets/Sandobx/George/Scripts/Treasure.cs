using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, ICollectable
{
    [SerializeField] private int pointsValue = 100;
    [SerializeField] private int id;
    public void OnCollection()
    {
        AudioManager.Instance.PlaySfx(id);
        gameObject.SetActive(false);
        GameManager.Instance.OnPointsGather(pointsValue, true);
        LevelManager.Instance.StartGoingUp();
    }
}
