using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Pirate's Booty
/// </summary>
public class Doubloon : Item
{
    public override void Start()
    {
        base.Start();
        Health = 0;
        Points = 10;
        itemType = ItemTypes.Doubloon;
        item_type = "doubloon";
        soundName = AudioClipName.DoubloonPickupNoise;
        gold = 1;
    }
}
