using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Greedy : Brain
{
    GameObject Pirate;


    public override Vector2 getNext()
    {
        Pirate = GameObject.Find("pirate_idle_0");
        if (Pirate != null)
        {
            newPosition = Vector2.MoveTowards(transform.position, Pirate.GetComponent<Rigidbody2D>().position, 10 * Time.deltaTime);
        }

        return newPosition;

    }

}
