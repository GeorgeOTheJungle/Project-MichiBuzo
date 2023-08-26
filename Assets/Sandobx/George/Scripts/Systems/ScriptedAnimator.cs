using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptedAnimator : MonoBehaviour
{
    [SerializeField] private float frameRate;
    
    [SerializeField] private float duration;

    [SerializeField] private Sprite[] frames;
     private SpriteRenderer spriteRenderer;
    private Image imageRenderer;
    private SpriteMask mask;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        imageRenderer = GetComponent<Image>();
        mask = GetComponent<SpriteMask>();
    }

    private void Start()
    {
        StartCoroutine(Animation());
    }

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        int f = 0;
        while(gameObject.activeSelf)
        {
            if(spriteRenderer) spriteRenderer.sprite = frames[f];
            if (imageRenderer) imageRenderer.sprite = frames[f];
            if (mask) mask.sprite = frames[f];
            f++;
            if (f >= frames.Length) f = 0;
            yield return new WaitForSeconds(frameRate);
        }
    }
}
