﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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


    void Update()
    {

    }

    public void setHighScore(int index, float score)
    {
        highScore.text = index.ToString() + ": " + score.ToString();
    }
   
    public void getInput()
    {
        print("HEY IM HERE!");
        update_initials = initials.text;
        print(update_initials);
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
}
