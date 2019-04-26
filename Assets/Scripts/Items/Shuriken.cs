using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A projectile thrown by Ninjas
/// </summary>
public class Shuriken : Item
{
    
    /// <summary>
    /// inherits Start() from Item class
    /// </summary>
    public override void Start()
    {
        base.Start();
        Health = -2;
        Points = -10;
        itemType = ItemTypes.Shuriken;
        soundName = AudioClipName.ShurikenHit;
        gold = 0;
        set_movement();
    }

   
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ARR"))
        {
            pickUp.Invoke(this);
            itemScoreEvent.Invoke(itemType, transform.position);
        }
        if (!collision.gameObject.CompareTag("Sneak"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {        
        transform.position = Vector2.MoveTowards(rb2d.position,
            direction, NinjaConfiguration.ThrowingArm * Time.deltaTime);
    }

    /// <summary>
    /// Sets direction of shuriken so it goes towards it's target 
    /// </summary>
    private void set_movement()
    {
        GameObject Pirate = GameObject.Find("pirate_idle_0");
        direction = new Vector2(Pirate.GetComponent<Rigidbody2D>().position.x, Pirate.GetComponent<Rigidbody2D>().position.y);
    }

}
