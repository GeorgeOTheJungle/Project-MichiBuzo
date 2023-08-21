using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, ICollectable
{
    public void OnCollection()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnTreasureGather(5000);
        LevelManager.Instance.StartGoingUp();
    }
}
