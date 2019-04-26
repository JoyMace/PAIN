using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Vitamin C while at Sea
/// </summary>
public class Lemon : Item
{
    public override void Start()
    {
        base.Start();
        Health = 0;
        Points = 5;
        itemType = ItemTypes.Lemon;
        item_type = "lemon";
        soundName = AudioClipName.LemonParty;
        gold = 0;
    }
}
