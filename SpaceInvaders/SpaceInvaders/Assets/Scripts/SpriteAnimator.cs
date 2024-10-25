using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    // Array med sprites som anv�nds f�r animationen
    public Sprite[] animationSprites = new Sprite[2];
    public float animationTime;

    SpriteRenderer spriteRenderer;
    int animationFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // St�ller in den f�rsta sprite fr�n animationSprites
        spriteRenderer.sprite = animationSprites[0];
    }
    private void Start()
    {
        // Startar en metod som kallas "animateSprite" upprepade g�nger med ett intervall av animationTime
        InvokeRepeating(nameof(animateSprite), animationTime, animationTime);
    }
    private void animateSprite()
    {
        animationFrame++;

        // Om vi n�tt slutet av arrayen, �terg�r vi till f�rsta sprite
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        // Uppdaterar SpriteRenderer:s sprite till n�sta i animationen
        spriteRenderer.sprite = animationSprites[animationFrame];
    }
}

