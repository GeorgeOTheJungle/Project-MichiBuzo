using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject visual;
    [SerializeField] private Transform levelTransform;
    private Collider2D col2D;
    private Transform playerPosition;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        col2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        visual.SetActive(false);
        col2D.enabled = false;
        transform.position = playerPosition.position;
    }

    private void Update()
    {
        if (targetPoint == null) return;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * CustomTime.DeltaTime);
    }

    public void LaunchWave(Transform target)
    {
        visual.SetActive(true);
        col2D.enabled = true;
        transform.SetParent(levelTransform);
        targetPoint = target;
    }

    private void ReturnToPlayer()
    {

        col2D.enabled = false;
        targetPoint = null;
        transform.position = playerPosition.position;
        transform.SetParent(playerPosition);

        visual.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        if (collision.CompareTag("Killable") || collision.CompareTag("Environment"))
        {
            if (collision.TryGetComponent<EnemyComponent>(out EnemyComponent enemy))
            {
                enemy.OnKill();
            }

            ReturnToPlayer();
        }

  
    }

}
