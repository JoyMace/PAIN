using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    GameObject Pirate;

    public Dictionary<LegalMoves, Vector2> legal = new Dictionary<LegalMoves, Vector2>();

    public Vector2 position;
    public Vector2 newPosition;
    public Vector2 goal;


    // called at initiation
    public virtual void Start()
    {
        Pirate = GameObject.Find("pirate_idle_0");
    }

    public virtual Vector2 getNext()
    {
        float speed_factor = NinjaConfiguration.MovementSpeed;
        Vector2 dummy = new Vector2(0, 0);
        legal.Clear();
        legal = get_legal_move(speed_factor);
        print(legal);
               
        return dummy;
    }

    public Dictionary<LegalMoves, Vector2> get_legal_move(float speed_factor)
    {
        // add north
        Vector2 north = transform.position;
        north.y += speed_factor;
        if (check_collision(north))
        {
            legal.Add(LegalMoves.North, north);
        }
        // add south
        Vector2 south = transform.position;
        south.y -= speed_factor;
        if (check_collision(south))
        {
            legal.Add(LegalMoves.South, south);
        }
        // add east
        Vector2 east = transform.position;
        east.x += speed_factor;
        if (check_collision(east))
        {
            legal.Add(LegalMoves.East, east);
        }
        // add west
        Vector2 west = transform.position;
        west.x -= speed_factor;
        if (check_collision(west))
        {
            legal.Add(LegalMoves.West, west);
        }
        // add stop
        legal.Add(LegalMoves.Stop, transform.position);

        return legal;
    }

    bool check_collision(Vector2 position)
    {
        Vector2 topLeft = position;
        topLeft.x -= 0.25f;
        topLeft.y += 0.25f;
        Vector2 bottomRight = position;
        bottomRight.x += 0.25f;
        bottomRight.y -= 0.25f;
        bool clear = Physics2D.OverlapArea(topLeft, bottomRight);
        return clear;
    }
}
