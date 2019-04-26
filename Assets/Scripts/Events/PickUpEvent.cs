using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An Event indicating an Item has been picked up
/// </summary>
public class PickUpEvent : UnityEvent<Item>
{
}
