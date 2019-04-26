using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Empty after the Pirate drinks his rum
/// </summary>
public class Bottle : Item
{
    //reference to Ninja game object
    GameObject Ninja;     
           
    public override void Start()
    {
        base.Start();
        Health = 0;
        Points = 100;
        itemType = ItemTypes.Bottle;
        item_type = "bottle";
        soundName = AudioClipName.BottleThrow;
        gold = 0;
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

    /// <summary>
    /// Aim it at the closest Ninja
    /// </summary>
    private void set_movement()
    {
        Ninja = FindClosestNinja();
        direction = new Vector2(Ninja.GetComponent<Rigidbody2D>().position.x, Ninja.GetComponent<Rigidbody2D>().position.y);
    }

    /// <summary>
    /// Finds nearest Ninja to aim Bottle throw
    /// </summary>
    /// <returns></returns>
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
            closest = go;
        }
        return closest;
    }
}
