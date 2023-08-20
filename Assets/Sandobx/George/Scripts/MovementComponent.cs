using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Header("Movement manager"), Space(10)]
    [SerializeField] private float movementSpeed;
 
    private Rigidbody2D rb2d;
    private void HandleMovement(Vector2 direction)
    {
       // rb2d.velocity = movementSpeed * 
    }
}
