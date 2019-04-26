using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greedy : MonoBehaviour
{
    GameObject Pirate;


    Rigidbody2D pirate_body;
    float North = 1f;
    int South = -1;
    int East = 1;
    int West = -1;

    Vector3 position;
    Vector3 newPosition;
    Vector3 goal;

    public Vector3 getNext()
    {
        Pirate = GameObject.Find("pirate_idle_0");
        position = transform.position;
        newPosition = transform.position;
        goal = new Vector3(Pirate.transform.position.x, Pirate.transform.position.y);

        getLOS();

        // get closer in y
        if (goal.y < position.y)
        {
            newPosition.y += South * 7f * Time.deltaTime;
        }
        else
        {
            newPosition.y += North * 7f * Time.deltaTime;
        }

        // get closer in x
        if (goal.x < position.x)
        {
            newPosition.x += West * 5.5f * Time.deltaTime;
        }
        else
        {
            newPosition.x += East * 5.5f * Time.deltaTime;
        }
        

        return newPosition;
    }

    void getLOS()
    {
        if (goal.y < position.y)
        {
            if(position.y-goal.y <= 1)
            {
                
            }
            
        }
        else
        {
            newPosition.y += North * 7f * Time.deltaTime;
        }
    }
}
