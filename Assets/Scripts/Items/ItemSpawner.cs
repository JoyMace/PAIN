using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Collision-free support
    Vector2 min = new Vector2();
    Vector2 max = new Vector2();
    float colliderSize = 0.32f;   


    // Start is called before the first frame update
    void Start()
    {
        // Locations of green Tiles on to which things can spawn

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

                
        ItemDict.Add(Lemon, 2);
        ItemDict.Add(Rum, 2);
        ItemDict.Add(Doubloon, 20);

        // Pick Up Event support
        EventManager.AddPickUpListener(SpawnItem);
        
        // Spawn all the things
        SpawnManyItems(ItemDict);
    }

    /// <summary>
    /// Spawns items in random locations at beginning of game
    /// </summary>
    /// <param name="ItemDict"></param>
    void SpawnManyItems(Dictionary<GameObject, int> ItemDict)
    {
        
        var random = new Random();

        // Spawns items on green tiles
        foreach (var item2spawn in ItemDict)
        {
            for (int i = item2spawn.Value-1; i >= 0; i--)
            {
                Vector3 position = SpawnLocations[(Random.Range(0, SpawnLocations.Count))];
                SetMinAndMax(position);
                if (Physics2D.OverlapArea(min, max) == null)
                {
                    Instantiate(item2spawn.Key, position, Quaternion.identity);                            
                }
                
            }
        }

        // Fills the rest of the green tiles with Doubloons
        foreach (Vector2 location in SpawnLocations)
        {
            SetMinAndMax(location);
            if (Physics2D.OverlapArea(min, max) == null)
            {
                Instantiate(Doubloon, location, Quaternion.identity);                
            }
        }
    }


    /// <summary>
    /// Spawns an item in a random location after it has been picked up
    /// </summary>
    void SpawnItem(Item item)
    {
        GameObject item2spawn = get_item2spawn(item.item_type);
        Vector3 position = SpawnLocations[(Random.Range(0, SpawnLocations.Count))];
        SetMinAndMax(position);
        if (Physics2D.OverlapArea(min, max) == null)
        {
            Instantiate(item2spawn, position, Quaternion.identity);            
        }

    }

    /// <summary>
    /// Don't spawn too much rum
    /// </summary>
    /// <param name="item_name"></param>
    /// <returns></returns>
    GameObject get_item2spawn(string item_name)
    {
       
        if (item_name == "rum")
        {
            return Rum;
        }
       
        else
        {
            return Lemon;
        }
    }

    /// <summary>
    /// Sets min/max corners of object to support collision free spawning
    /// </summary>
    /// <param name="position"></param>
    void SetMinAndMax(Vector3 position)
    {
        min.x = position.x - colliderSize;
        min.y = position.y - colliderSize;
        max.x = position.x + colliderSize;
        max.y = position.y + colliderSize;
    }
}
