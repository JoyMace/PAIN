using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages Menu changes
/// </summary>
public static class MenuManager
{
    public static void GoToMenu(MenuName menu)
    {
        switch (menu)
        {
            case MenuName.Difficulty:
                // go to Difficulty Menu scene
                SceneManager.LoadScene("DifficultyMenu");
                break;

            case MenuName.Help:
                // go to Help Menu scene
                SceneManager.LoadScene("HelpMenu");
                break;

            case MenuName.Main:
                // go to Main Menu scene
                SceneManager.LoadScene("MainMenu");
                break;

            case MenuName.Pause:
                // instantiate prefab
                Object.Instantiate(Resources.Load("PauseMenu"));
                break;

            case MenuName.HighScore:
                SceneManager.LoadScene("HighScores");
                break;

            case MenuName.Gameplay:
                SceneManager.LoadScene("Gameplay");
                break;

            case MenuName.Credits:
                SceneManager.LoadScene("CreditsMenu");
                break;
        }
    }

}

