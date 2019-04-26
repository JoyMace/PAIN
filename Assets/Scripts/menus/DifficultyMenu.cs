using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    /// <summary>
    /// Select Easy AI Mode
    /// </summary>
    public void Easy()
    {
        Configuration.Difficulty =DifficultyLevels.Easy;
        NinjaConfiguration.set_Config(DifficultyLevels.Easy);
        Score.getScore = 0;
        MenuManager.GoToMenu(MenuName.Gameplay);
        AudioManager.Play(AudioClipName.MenuButtonClick);
    }

    /// <summary>
    /// Select Medium AI Mode
    /// </summary>
    public void Medium()
    {
        Configuration.Difficulty = DifficultyLevels.Medium;
        NinjaConfiguration.set_Config(DifficultyLevels.Medium);
        Score.getScore = 0;
        MenuManager.GoToMenu(MenuName.Gameplay);
        AudioManager.Play(AudioClipName.MenuButtonClick);

    }

    /// <summary>
    /// Select Hard AI Mode
    /// </summary>
    public void Hard()
    {
        Configuration.Difficulty = DifficultyLevels.Hard;
        NinjaConfiguration.set_Config(DifficultyLevels.Hard);
        Score.getScore = 0;
        MenuManager.GoToMenu(MenuName.Gameplay);
        AudioManager.Play(AudioClipName.MenuButtonClick);

    }

    /// <summary>
    /// Go Back to Main Menu
    /// </summary>
    public void Back()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.MenuButtonClick);

    }

    /// <summary>
    /// Quit Game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
        AudioManager.Play(AudioClipName.MenuButtonClick);

    }
}
