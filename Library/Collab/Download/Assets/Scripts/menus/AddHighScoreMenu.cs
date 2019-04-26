using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AddHighScoreMenu :MonoBehaviour
{
    [SerializeField]
    Text highScore;

    [SerializeField]
    InputField initials;

    GameOverEvent gameOverEvent = new GameOverEvent();
    NewHighScoreEvent highScoreEvent = new NewHighScoreEvent();
    static int leader_board_position = 999;
    string update_initials;

    #region Procedures
    private void Start()
    {
        Time.timeScale = 0;
        EventManager.AddHighScoreInvoker(this);
    }

    public void setHighScore(int index, float score)
    {
        highScore.text = index.ToString() + ": " + score.ToString();
    }
   
    public void getInput()
    {
        update_initials = initials.text;
        float score = Score.getScore;
        highScoreEvent.Invoke(update_initials);
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.HighScore);
    }

    public static int LeaderBoardPosition
    {
        get { return leader_board_position; }
    }
    #endregion

    public void AddHighScoreListener(UnityAction<string> listener)
    {
        highScoreEvent.AddListener(listener);
    }

    /// <summary>
    /// Handles Quit button click
    /// </summary>
    public void ExitGame()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Application.Quit();
    }

    /// <summary>
    /// Handles Back button click
    /// </summary>
    public void GoBack()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.MenuButtonClick);
    }
}
