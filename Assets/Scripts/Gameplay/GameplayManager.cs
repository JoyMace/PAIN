using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gameplay manager
/// </summary>
public class GameplayManager : MonoBehaviour
{
    private void Start()
    {
        // game over support        
        EventManager.AddGameOverListener(HandleGameOverEvent);

    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //pause game on escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    /// <summary>
    /// What to do if game over event is triggered
    /// </summary>
    /// <param name="winner"></param>
    void HandleGameOverEvent(String winner)
    {
        Score.checkHighScore();
        int leader_board_position = Score.UpdatePostion;
        float score = Score.getScore;        
        print(leader_board_position);


        if (leader_board_position < 11)
        {
            GameObject newHighScore = (GameObject)Instantiate(Resources.Load("AddHighScore"));
            newHighScore.GetComponent<AddHighScoreMenu>().setHighScore(score);
        }
        else
        {
            GameObject gameOverMessage = (GameObject)Instantiate(Resources.Load("GameOverMenu"));
            gameOverMessage.GetComponent<GameOverMenu>().SetWinner(winner);
        }
    }
}
