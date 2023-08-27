using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour, ICollectable
{
    [SerializeReference] private float oxygenCollectionValue;
    [SerializeField] private int id;
    public void OnCollection()
    {
        OxygenComponent.Instance.OnOxygenCollection(oxygenCollectionValue);
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySfx(id);
    }
}
