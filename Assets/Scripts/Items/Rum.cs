using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An alcoholic beverage. Drink Responsibly. 
/// Always aim for Ninjas while Drunk.
/// </summary>
public class Rum : Item
{
    public override void Start()
    {
        base.Start();
        Health = 2;
        Points = 5;
        itemType = ItemTypes.Rum;
        item_type = "rum";
        soundName = AudioClipName.DrinkRum;
        gold = 0;
    }
}
