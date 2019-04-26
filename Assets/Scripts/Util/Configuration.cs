using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{

    private static DifficultyLevels difficulty = DifficultyLevels.Easy;


    // initialize high score list
    public static void InitializeHighScores()
    {
        int index;
        for (index = 1; index < 11; index++)
        {
            if (!PlayerPrefs.HasKey(index + " HighScore"))
            {
                PlayerPrefs.SetFloat(index + " HighScore", 0);
                PlayerPrefs.SetString(index + " HighScoreName", "***");
            }
        }
    }



    public static DifficultyLevels Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
}
