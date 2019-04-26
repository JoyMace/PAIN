using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pirate's Booty Box
/// </summary>
public class Chest : MonoBehaviour
{
    static bool level_completed = false;
    
    /// <summary>
    /// Chest Opens when Pirate has collected all Doubloons
    /// </summary>
    /// <param name="indicator"></param>
    public void setComplete(bool indicator)
    {
        level_completed = indicator;
        gameObject.GetComponent<Animate>().setOnEvent(false);
    }

    public static bool levelCompleted
    {
        get { return level_completed; }
    }
}
