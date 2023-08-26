using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemyComponent : EnemyComponent
{
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float moveSpeed = 1.5f;

    private Transform playerTransform;
    private Vector2 playerPos;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.FindGameObjectWithTag("FollowPoint").GetComponent<Transform>();
    }

    private void Update()
    {
        if (canMove == false) return;
        Vector2 playerPositionRelative = playerTransform.InverseTransformPoint(transform.position);
        if(Vector2.Distance(transform.position, playerTransform.position) < attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * CustomTime.DeltaTime);

            if (transform.position.x > playerTransform.position.x && isLeft) Flip();
            else if (transform.position.x < playerTransform.position.x && !isLeft) Flip();
        }
        
    }

    public override void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

}
