using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pause Menu pops up on ESC button press
/// </summary>
public class PauseMenu : MonoBehaviour
{
    GameObject music;

    private void Start()
    {
        music = GameObject.FindGameObjectWithTag("BackgroundMusic");
        //pause the game when added to the scene
        music.GetComponent<AudioSource>().Pause();
        Time.timeScale = 0;        
    }
    /// <summary>
    /// Resumes the paused game
    /// </summary>
    public void ResumeGame()
    {
        //unpause game and destroy pause menu
        music.GetComponent<AudioSource>().Play();
        Time.timeScale = 1;
        Destroy(gameObject);       

    }

    /// <summary>
    /// Quits the paused game
    /// </summary>
    public void QuitGame()
    {
        //unpause game, destroy pause menu, go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
