using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingEnemyComponent : EnemyComponent
{
    [Header("Wander Enemy Component:"), Space(10)]
    [SerializeField] private float range;

    private bool goingLeft;

    private Vector2 pointA, pointB;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x - range,transform.position.y), new Vector2(transform.position.x + range, transform.position.y));
    }

    public override void Start()
    {
        base.Start();
        pointA = new Vector2(transform.position.x - range, transform.localPosition.y);
        pointB = new Vector2(transform.position.x + range, transform.localPosition.y);

        goingLeft = Random.Range(0, 2) == 1;
    }

    private void Update()
    {
        if (canMove == false) return;
        Wander();
    }

    public override void OnKill()
    {
        base.OnKill();
        canMove = false;
    }

    private void Wander()
    {
        if (goingLeft)
        {
            if (Vector2.Distance(transform.localPosition, pointA) > 0.01f)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, pointA, movementSpeed * CustomTime.DeltaTime);
            }
            else
            {
                goingLeft = false;
                if (!isLeft) Flip();
            }
        } else
        {
            if(Vector2.Distance(transform.localPosition, pointB) > 0.01f)
            {
                transform.localPosition = Vector2.MoveTowards(transform.localPosition, pointB, movementSpeed * CustomTime.DeltaTime);
            } else
            {
                goingLeft = true;
                if(isLeft) Flip();
            }
        }
    }
}
