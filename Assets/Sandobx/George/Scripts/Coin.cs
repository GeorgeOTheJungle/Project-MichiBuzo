using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private int pointsValue;
    public void OnCollection()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnPointsGather(pointsValue,false);
    }

}
