using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main Menu handling
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts Game
    /// </summary>
    public void StartGame()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Score.getScore = 0;
        NinjaConfiguration.set_Config(DifficultyLevels.Easy);
        MenuManager.GoToMenu(MenuName.Gameplay);
    }

    /// <summary>
    /// Shows High Scores
    /// </summary>
    public void ShowHighScores()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.HighScore);
    }
    /// <summary>
    /// Goes to help screen
    /// </summary>
    public void ShowHelp()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Help);
    }

    /// <summary>
    /// Goes to Difficulty screen
    /// </summary>
    public void ChooseDifficulty()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Difficulty);
    }

    /// <summary>
    /// goes to credits screen
    /// </summary>
    public void ViewCredits()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.Credits);
    }
    /// <summary>
    /// Exits game
    /// </summary>
    public void ExitGame()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Application.Quit();
    }
}
