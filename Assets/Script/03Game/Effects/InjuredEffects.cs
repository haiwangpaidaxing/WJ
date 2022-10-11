using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredEffects : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
      //  spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        Destroy(gameObject,0.4f);
    }
}
