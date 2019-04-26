using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Item
{
   
    public override void Start()
    {
        base.Start();
        set_movement();
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ARR"))
        {
            pickUpEvent.Invoke(item_name);
        }
        if (!collision.gameObject.CompareTag("Sneak"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        transform.position = Vector2.MoveTowards(rb2d.position, direction, 10*Time.deltaTime);
    }

    private void set_movement()
    {
        GameObject Pirate = GameObject.Find("pirate_idle_0");
        direction = new Vector2(Pirate.GetComponent<Rigidbody2D>().position.x, Pirate.GetComponent<Rigidbody2D>().position.y);
    }

}
