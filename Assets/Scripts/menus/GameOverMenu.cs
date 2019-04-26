using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The game over message
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    Text messageText;
    [SerializeField]
    Text scoreText;

    GameObject music;

    float score;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("BackgroundMusic");
        // pause the game when added to the scene
        music.GetComponent<AudioSource>().Pause();
        Time.timeScale = 0;
        float score = Score.getScore;
        score = Score.getScore;
        scoreText.text = "Score " + score.ToString();
    }

    /// <summary>
    /// Sets the winning side in the message to the given side
    /// </summary>
    /// <param name="winner">winner</param>
    public void SetWinner(String winner)
    {
        // if Pirate is alive and gets to the Treasure chest, Pirate wins
        if(winner == "pirate")
        {
            messageText.text = "ARRRRRRGH A Pirate's Life For ME";
        }
        //else if Pirate dies, Ninjas win
        else if (winner== "ninja")
        {
            messageText.text = "NINJAS REIGN SUPREME";
        }
    }

    /// <summary>
    /// Moves to main menu when quit button clicked
    /// </summary>
    void HandleQuitButtonClicked()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.MenuButtonClick);
    }

    public void HandleHighScoresButtonClicked()
    {
        MenuManager.GoToMenu(MenuName.HighScore);
        AudioManager.Play(AudioClipName.MenuButtonClick);
    }
}
