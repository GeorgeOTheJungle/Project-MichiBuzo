using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private int pointsValue;
    [SerializeField] private int id;
    public void OnCollection()
    {
        AudioManager.Instance.PlaySfx(id);
        gameObject.SetActive(false);
        GameManager.Instance.OnPointsGather(pointsValue,false);
    }

}
