using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AddHighScoreMenu :MonoBehaviour
{
    // shows the player's new high score
    [SerializeField]
    Text highScore;


    GameOverEvent gameOverEvent = new GameOverEvent();
    NewHighScoreEvent highScoreEvent = new NewHighScoreEvent();
    string update_initials;

    #region methods

    /// <summary>
    /// The Add high score Menu initializes with
    /// - pauses game time 
    /// - set invoker to add a high score
    /// </summary>
    private void Start()
    {
        Time.timeScale = 0;
        EventManager.AddHighScoreInvoker(this);

    }

    /// <summary>
    /// Sets the text to display a high score
    /// </summary>
    /// <param name="score"></param>
    public void setHighScore(float score)
    {
        highScore.text = "Score: " + score.ToString();
    }
   

    /// <summary>
    /// - gets player name from game object
    /// - uses the name to invoke the event to add a new high score
    /// - unpauses game, returns to High Scores menu and deletes current game object.
    /// </summary>
    /// <param name="new_text"></param>
    public void getInput(string new_text)
    {
        update_initials = new_text;
        float score = Score.getScore;
        highScoreEvent.Invoke(update_initials);
        Time.timeScale = 1;
        AudioManager.Play(AudioClipName.MenuButtonClick);
        MenuManager.GoToMenu(MenuName.HighScore);
        Destroy(gameObject);

    }

    /// <summary>
    /// adds listeners for add high score event
    /// </summary>
    /// <param name="listener"></param>
    public void AddHighScoreListener(UnityAction<string> listener)
    {
        highScoreEvent.AddListener(listener);
    }

    #endregion
}
