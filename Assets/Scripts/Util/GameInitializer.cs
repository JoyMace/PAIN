using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        // checks for high score list, calls Config to build if it has not been initialized 
        if (!PlayerPrefs.HasKey(0 + " HighScore"))
        {
            Configuration.InitializeHighScores();
        }
    }
}
