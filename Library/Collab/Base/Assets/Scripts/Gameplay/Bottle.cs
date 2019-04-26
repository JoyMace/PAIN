using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Item
{
    GameObject Ninja;

    public override void setName()
    {
        item_name = "bottle";
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        set_movement();

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void Update()
    {

        transform.position = Vector2.MoveTowards(rb2d.position, direction, 5*Time.deltaTime);
    }

    private void set_movement()
    {
        Ninja = FindClosestNinja();
        direction = new Vector2(Ninja.GetComponent<Rigidbody2D>().position.x, Ninja.GetComponent<Rigidbody2D>().position.y);
    }

    public GameObject FindClosestNinja()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Sneak");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
