using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private Transform levelTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        transform.SetParent(levelTransform);
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }
}
