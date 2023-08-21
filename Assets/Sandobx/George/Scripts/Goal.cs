using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.goalGO = gameObject;
        gameObject.SetActive(false);
    }
}
