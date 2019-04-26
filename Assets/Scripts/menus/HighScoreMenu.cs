using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMenu : MonoBehaviour
{
    // the text field that will show the high scores
    [SerializeField]
    Text high_scores;

    /// <summary>
    /// initializes with cached list of high score values
    /// </summary>
    void Start()
    {
        getHighScores();
    }

    /// <summary>
    /// retrieves cache'd scores and creates a list format by index number
    /// </summary>
    void getHighScores()
    {
        int index;
        for (index = 1; index < 11; index++)
        {
            string name = PlayerPrefs.GetString(index + " HighScoreName");
            string score = PlayerPrefs.GetFloat(index + " HighScore").ToString();
            high_scores.text += index.ToString() + ". " + name + "................" + score + "\n";
        }
    }

    /// <summary>
    /// returns user to main menu
    /// </summary>
    public void returnClicked()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.MenuButtonClick);

    }

    public void QuitClicked()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Application.Quit();
    }
}
