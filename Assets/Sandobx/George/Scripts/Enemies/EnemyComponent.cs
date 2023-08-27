using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class EnemyComponent : MonoBehaviour,ITargeteable
{
    [Header("Enemy Settings: "),Space(10)]
    // TODO Kill system
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected int pointsOnKill;

    [SerializeField] private GameObject enemyVisual;
    [SerializeField] private GameObject pointsTextVisual;
    [SerializeField] protected int id;
    private TextMeshPro pointsText;

    private Collider2D col2D;
    protected float movementSpeed;

    public virtual void Awake()
    {
        pointsText = pointsTextVisual.GetComponentInChildren<TextMeshPro>();

        pointsText.SetText(pointsOnKill.ToString());
        col2D = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }
    public virtual void OnKill()
    {
        canMove = false;
        AudioManager.Instance.PlaySfx(id);
        enemyVisual.SetActive(false);
        pointsTextVisual.SetActive(true);
        col2D.enabled = false;
        Invoke("Dissapear", 1.5f);
    }

    private void Dissapear()
    {
        gameObject.SetActive(false);
    }

    protected bool isLeft;
    protected void Flip()
    {
        isLeft = !isLeft;
        Vector2 newScale;
        if (isLeft)
        {
            newScale =new Vector2(-1f,1f);
            transform.localScale = newScale;
        } else
        {
            newScale = new Vector2(1f, 1f);
            transform.localScale = newScale;
        }
    }

}
