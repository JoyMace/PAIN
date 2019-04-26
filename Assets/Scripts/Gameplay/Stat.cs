using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used by UI bars to display information about the player
/// </summary>
public class Stat : MonoBehaviour
{
    /// <summary>
    /// The image we are updating
    /// </summary>
    private Image content;

    /// <summary>
    /// Reference to stat text on bar
    /// </summary>
    [SerializeField]
    private Text statValue;

    /// <summary>
    /// Current amount that the bar displays
    /// </summary>
    private float currentFill;

    /// <summary>
    /// Speed at which the bar fills
    /// </summary>
    [SerializeField]
    private float lerpSpeed;

    /// <summary>
    /// The stat's max value
    /// </summary>
    public float MaxValue { get; set; }

    /// <summary>
    /// current value of the stat, not of the bar
    /// </summary>
    private float currentValue;

    #region Properties
    /// <summary>
    /// allows other classes to access this stat and set/get the value
    /// </summary>
    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MaxValue)//don't go over max value
            {
                currentValue = MaxValue;
            }
            else if(value < 0)//can't be a negative value
            {
                currentValue = 0;
            }
            else//if the value we're trying to set is within max/min then we set it
            {
                currentValue = value;
            }

            //Calculates the currentFill, so that we can lerp
            currentFill = currentValue / MaxValue;

            //updates the bar with the correct text           
            statValue.text = currentValue + " / " + MaxValue;           
            
        }
    }
    #endregion
       

    // Start is called before the first frame update
    void Start()
    {
       
        //get reference to image in UI
        content = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //update the bar properly
        HandleBar();
    }

    /// <summary>
    /// initialize Health Bar to correct value on Start
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="maxValue"></param>
    public void Initialize(float currentValue, float maxValue)
    {
        MaxValue = maxValue;
        MyCurrentValue = currentValue;
    }

    private void HandleBar()
    {
        if (currentFill != content.fillAmount) //If we have a new fill amount then we know that we need to update the bar
        {
            //Lerps the fill amount so that we get a smooth movement
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }
}
