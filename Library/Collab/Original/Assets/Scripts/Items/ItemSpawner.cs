using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Spawns Items
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject Lemon;
    [SerializeField]
    GameObject Rum;
    [SerializeField]
    GameObject Doubloon;
    
    /// <summary>
    /// List of Tiles upon which respawns are appropriate
    /// </summary>
    List<Vector3> SpawnLocations = new List<Vector3>();
        
    /// <summary>
    /// Objects that are spawnable
    /// </summary>
    Dictionary<GameObject, int> ItemDict = new Dictionary<GameObject, int>();
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnLocations.Add(new Vector3(-14f, 6.3f));
        SpawnLocations.Add(new Vector3(-14f, 3.5f));
        SpawnLocations.Add(new Vector3(-14f, -3.05f));

        SpawnLocations.Add(new Vector3(-11.3f, 6.3f));
        SpawnLocations.Add(new Vector3(-11.3f, -7.0f));

        SpawnLocations.Add(new Vector3(-4.68f, 6.3f));
        SpawnLocations.Add(new Vector3(-4.68f, 3.5f));
        SpawnLocations.Add(new Vector3(-4.68f, -0.4f));
        SpawnLocations.Add(new Vector3(-4.68f, -4.36f));
        SpawnLocations.Add(new Vector3(-4.68f, -7.0f));

        SpawnLocations.Add(new Vector3(-2.05f, 6.3f));
        SpawnLocations.Add(new Vector3(-2.05f, -7.0f));

        SpawnLocations.Add(new Vector3(2.0f, 6.2f));
        SpawnLocations.Add(new Vector3(2.0f, -7.0f));

        SpawnLocations.Add(new Vector3(4.61f, 6.3f));
        SpawnLocations.Add(new Vector3(4.61f, 3.5f));
        SpawnLocations.Add(new Vector3(4.61f, -0.4f));
        SpawnLocations.Add(new Vector3(4.61f, -4.36f));
        SpawnLocations.Add(new Vector3(4.61f, -7.0f));

        SpawnLocations.Add(new Vector3(11.3f, 6.3f));

        SpawnLocations.Add(new Vector3(14f, 6.3f));
        SpawnLocations.Add(new Vector3(14f, 3.5f));
        SpawnLocations.Add(new Vector3(14f, -3.05f));
        SpawnLocations.Add(new Vector3(14f, -7.0f));

                
        ItemDict.Add(Lemon, 3);
        ItemDict.Add(Rum, 2);
        ItemDict.Add(Doubloon, 10);

        EventManager.AddPickUpListener(SpawnItem);
        
        SpawnManyItems(ItemDict);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns items in random locations at beginning of game
    /// </summary>
    /// <param name="ItemDict"></param>
    void SpawnManyItems(Dictionary<GameObject, int> ItemDict)
    {
        // TO DO Make sure they don't spawn into collisions with other items
        
        var random = new Random();

        foreach(var item2spawn in ItemDict)
        {
            for (int i = item2spawn.Value; i >= 0; i--)
            {
                Vector3 position = SpawnLocations[(int)(Random.Range(0, SpawnLocations.Count))];

                Instantiate(item2spawn.Key, position, Quaternion.identity);
                print("SPAWNING " + item2spawn.Key + " AT LOCATION " + position);
            }            
        }
    }

    
    /// <summary>
    /// Spawns an item in a random location after it has been picked up
    /// </summary>
    void SpawnItem(string itemName)
    {
        if (itemName != "shuriken")
        {
            GameObject item2spawn = GameObject.FindGameObjectWithTag(itemName);
            Vector3 position = new Vector3(Random.Range(1, 2), Random.Range(1, 2), Random.Range(1, 2));
            Instantiate(item2spawn, position, Quaternion.identity);
        }
        
    }

    bool check_collision(Vector3 position)
    {
        Vector3 topLeft = position;
        topLeft.x -= 0.25f;
        topLeft.y += 0.25f;
        Vector3 bottomRight = position;
        bottomRight.x += 0.25f;
        bottomRight.y -= 0.25f;
        bool clear = Physics2D.OverlapArea(topLeft, bottomRight);
        return clear;
    }
}
