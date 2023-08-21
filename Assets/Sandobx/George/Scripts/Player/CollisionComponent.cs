using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    private MovementComponent movementComponent;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            GameManager.Instance.OnPlayerHit();

            // Art change here.
            // Sound goes here.
        } else if (collision.CompareTag("Collectable"))
        {
            collision.GetComponent<ICollectable>().OnCollection();
        } else if (collision.CompareTag("Goal"))
        {
            GameManager.Instance.OnLevelFinished();
        }
    }
}
