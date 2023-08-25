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

        visual.transform.right = target.position - transform.position;
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        visual.transform.localScale = new Vector2(.25f, .25f);
        float scaleX = .25f;

        while(scaleX < 1.0f)
        {
            scaleX += 2.5f * CustomTime.DeltaTime;
            visual.transform.localScale = new Vector2(scaleX, scaleX);
            yield return new WaitForEndOfFrame();
        }
        visual.transform.localScale = new Vector2(1f, 1f);
    }

    private void ReturnToPlayer()
    {

        col2D.enabled = false;
        targetPoint = null;
        transform.position = playerPosition.position;
        transform.SetParent(playerPosition);

        visual.SetActive(false);
        StopCoroutine(ScaleAnimation());

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
