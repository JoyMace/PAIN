using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// The Player's Score
/// </summary>
public class Score : MonoBehaviour
{
    // Links Score to pop up object to create floating scores
    [SerializeField]
    GameObject popUp;
    [SerializeField]
    Text currentScore;

    static float score = 0;

    static float additionalPoints;

    Dictionary<ItemTypes, float> itemScore = new Dictionary<ItemTypes, float>();
    static int leader_board_position;
 
    // Start is called before the first frame update
    void Start()
    {
        buildDictionary();
        currentScore.text = "Score: " + score;
        EventManager.AddHighScoreListener(UpdateHighScore);
        EventManager.AddItemScoreListener(update_item_score);
        EventManager.AddNinjaScoreListener(update_ninja_score);

    }

    private void Update()
    {
        currentScore.text = "Score: " + score;
    }

    public static void checkHighScore()
    {
        int index;
        leader_board_position = 999;
        //float update_score = score;
        for (index = 1; index < 11; index++)
        {
            if (PlayerPrefs.HasKey(index + " HighScore"))
            {
                if (PlayerPrefs.GetFloat(index + " HighScore") < score)
                {
                    if (leader_board_position == 999)
                    {
                        print(PlayerPrefs.GetFloat(index + " HighScore"));
                        print(score);
                        leader_board_position = index;
                        print(index);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Update the Highest score on the board
    /// </summary>
    /// <param name="name"></param>
    public void UpdateHighScore(string name)
    {
        string update_name = name;
        float update_score = score;
        int index;
        for (index = 1; index < 11; index++)
        {
            if (PlayerPrefs.HasKey(index + " HighScore"))
            {
                if (PlayerPrefs.GetFloat(index + " HighScore") < update_score)
                {
                    //change this to an invoker 
                    // new score is higher than the stored score
                    float temp_score = PlayerPrefs.GetFloat(index + " HighScore");
                    string temp_name = PlayerPrefs.GetString(index + " HighScoreName");
                    PlayerPrefs.SetFloat(index + " HighScore", update_score);
                    PlayerPrefs.SetString(index + " HighScoreName", update_name);
                    update_score = temp_score;
                    update_name = temp_name;
                }
            }
            else
            {
                PlayerPrefs.SetFloat(index + " HighScore", update_score);
                PlayerPrefs.SetString(index + " HighScoreName", update_name);
                update_score = 0;
                update_name = "";
            }
        }
    }

    /// <summary>
    /// Dict of items and scores associated with them
    /// </summary>
    void buildDictionary()
    {
        itemScore.Add(ItemTypes.Bottle, 100);
        itemScore.Add(ItemTypes.Doubloon, 10);
        itemScore.Add(ItemTypes.Rum, 5);
        itemScore.Add(ItemTypes.Lemon, 5);
        itemScore.Add(ItemTypes.Shuriken, -10);
    }
    void update_item_score(ItemTypes item, Vector2 position)
    { 
        float add = itemScore[item];
        score += add;
        additionalPoints = add;
        Vector3 screen_pos = Camera.main.WorldToScreenPoint(position);
        Vector2 new_position;
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screen_pos, canvas.worldCamera, out new_position);
        var pop = Instantiate(popUp, canvas.transform.TransformPoint(new_position), Quaternion.identity, canvas.transform);
    }

    /// <summary>
    /// Adds score based on how Ninja was defeated
    /// </summary>
    /// <param name="causeOfDeath"></param>
    /// <param name="position"></param>
    void update_ninja_score(string causeOfDeath, Vector2 position)
    {
        float add = 100;
        if (causeOfDeath == "drunk")
        { add = 150; }
        score += add;
        additionalPoints = add;
        Vector3 screen_pos = Camera.main.WorldToScreenPoint(position);
        Vector2 new_position;
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screen_pos, canvas.worldCamera, out new_position);
        var pop = Instantiate(popUp, canvas.transform.TransformPoint(new_position), Quaternion.identity, canvas.transform);

    }

    #region Properties

    public static float getScore
    {
        get { return score; }
        set { score = value; }
    }

    public static float getCurrent
    {
        get { return additionalPoints; }
    }

    public static int UpdatePostion
    {
        get { return leader_board_position;}
    }

    #endregion

}
