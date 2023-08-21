using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollection()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnCoinGather();
    }

}
