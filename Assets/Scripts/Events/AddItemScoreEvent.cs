using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event to add Item based scores
/// </summary>
public class AddItemScoreEvent : UnityEvent<ItemTypes, Vector2> 
{
}
