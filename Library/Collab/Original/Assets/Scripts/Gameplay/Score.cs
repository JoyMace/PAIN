using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    GameObject popUp;
    static float score = 0;

    static float additionalPoints;
    public Text currentScore;

    Dictionary<ItemTypes, float> itemScore = new Dictionary<ItemTypes, float>();

    AddItemScoreEvent itemScoreEvent = new AddItemScoreEvent();
    AddNinjaScoreEvent ninjaScoreEvent = new AddNinjaScoreEvent();
    // Start is called before the first frame update
    void Start()
    {
        buildDictionary();
        //additionalPoints.text = "0";
        //currentScore.text = "0";
        EventManager.AddItemScoreListener(update_item_score);
    }

    void buildDictionary()
    {
        itemScore.Add(ItemTypes.bottle, 100);
        itemScore.Add(ItemTypes.doubloon, 10);
        itemScore.Add(ItemTypes.rum, 5);
        itemScore.Add(ItemTypes.lemon, 5);
        itemScore.Add(ItemTypes.shuriken, -10);
    }
    void update_item_score(ItemTypes item, Vector2 position)
    {
        print("SCORING");
        float add = itemScore[item];
        score += add;
        additionalPoints = add;
        
    }

    public static float getScore
    {
        get { return score; }
    }

    public static float getCurrent
    {
        get { return additionalPoints; }
    }

}
