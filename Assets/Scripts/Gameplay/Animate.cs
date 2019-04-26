using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animates GameObjects
/// </summary>
public class Animate : MonoBehaviour
{

    // animate pirate 
    [SerializeField]
    string spriteNames;
    [SerializeField]
    bool onEvent;

    int sprite_version = 0;
    SpriteRenderer spriteRenderer;
    Sprite[] sprites;
    Timer animate_timer;

    // Start is called before the first frame update
    void Start()
    {
        // for animation
        animate_timer = gameObject.AddComponent<Timer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>(spriteNames);

    }

    // Update is called once per frame
    void Update()
    {
        if (onEvent == false)
        {
            AnimateTimer();
        }

    }

    /// <summary>
    /// Length of animation
    /// </summary>
    void AnimateTimer()
    {
        if (animate_timer.Running == false)
        {
            animate_timer.Duration = 0.2f;
            animate_timer.Run();
            animate_timer.timerFinishedEventListener(AnimatePirate);
        }
    }

    /// <summary>
    /// Pirate specific Animation
    /// </summary>
    void AnimatePirate()
    {
        sprite_version += 1;
        if (sprite_version > sprites.Length - 1)
        {
            sprite_version = 0;
        }
        spriteRenderer.sprite = sprites[sprite_version];

    }    

    public void setOnEvent(bool indicator)
    {
        onEvent = indicator;
    }
}
