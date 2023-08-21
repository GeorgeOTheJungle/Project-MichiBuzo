using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyComponent : MonoBehaviour
{
    [Header("Enemy Settings: "),Space(10)]
    // TODO Kill system
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    protected float movementSpeed;

    public virtual void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }
    public abstract void OnKill();

    
}
