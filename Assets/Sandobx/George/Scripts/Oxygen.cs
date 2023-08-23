using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour, ICollectable
{
    [SerializeReference] private float oxygenCollectionValue;
    public void OnCollection()
    {
        OxygenComponent.Instance.OnOxygenCollection(oxygenCollectionValue);
        gameObject.SetActive(false);
    }
}
