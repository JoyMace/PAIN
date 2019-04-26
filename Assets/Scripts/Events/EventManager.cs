using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event manager: example of Observer Pattern
/// </summary>

public static class EventManager
{
    #region Fields

    /// <summary>
    /// Pick Up Event
    /// Items Invoke, pirate method listens, uses string name of item to evaluate
    /// </summary>
    static List<Item> pick_up_invoker = new List<Item>();
    static List<UnityAction<Item>> pick_up_listener = new List<UnityAction<Item>>();

    // ninja respawn event
    static List<Ninja> respawn_invoker = new List<Ninja>();
    static List<UnityAction> respawn_listener = new List<UnityAction>();

    // game over event
    static List<Pirate> gameOverInvokers = new List<Pirate>();
    static List<UnityAction<string>> gameOverListeners =
        new List<UnityAction<string>>();

    // add item score event
    static List<Item> item_score_invokers = new List<Item>();
    static List<UnityAction<ItemTypes, Vector2>> item_score_listeners = new List<UnityAction<ItemTypes, Vector2>>();

    // add ninja score event
    static List<Ninja> ninja_score_invokers = new List<Ninja>();
    static List<UnityAction<string, Vector2>> ninja_score_listeners = new List<UnityAction<string, Vector2>>();

    static List<AddHighScoreMenu> highScoreInvokers = new List<AddHighScoreMenu>();
    static List<UnityAction<string>> highScoreListeners = new List<UnityAction<string>>();

    #endregion

    #region PickUpMethods

    /// <summary>
    /// Adds invokers of Pickup Events
    /// </summary>
    /// <param name="item"></param>
    public static void AddPickUpInvoker(Item item)
    {
        pick_up_invoker.Add(item);
        foreach (UnityAction<Item> listener in pick_up_listener)
        {
            item.AddPickUpListener(listener);
        }
    }

    /// <summary>
    ///  Adds a pick up listener
    /// </summary>
    /// <param name="handeler"> event handeler </param>
    public static void AddPickUpListener(UnityAction<Item> handeler)
    {
        pick_up_listener.Add(handeler);
        foreach (Item item in pick_up_invoker)
        {
            item.AddPickUpListener(handeler);
        }
    }

    #endregion

    #region NinjaRespawn methods
    /// <summary>
    /// Adds a respawn invoker
    /// </summary>
    /// <param name="script"> pick up script </param>
    public static void AddTResapwnInvoker(Ninja script)
    {
        respawn_invoker.Add(script);
        foreach (UnityAction listener in respawn_listener)
        {
            script.RespawnListener(listener);
        }
    }

    /// <summary>
    ///  Adds a throw listener
    /// </summary>
    /// <param name="handeler"> event handeler </param>
    public static void AddRespawnListener(UnityAction handeler)
    {
        respawn_listener.Add(handeler);
        foreach (Ninja ninja in respawn_invoker)
        {
            ninja.RespawnListener(handeler);
        }
    }
    #endregion

    #region Game Over support

    public static void AddGameOverInvoker(Pirate invoker)
    {
        gameOverInvokers.Add(invoker);
        foreach (UnityAction<String> listener in gameOverListeners)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the given method as a game over listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddGameOverListener(UnityAction<String> listener)
    {
        gameOverListeners.Add(listener);
        foreach (Pirate invoker in gameOverInvokers)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    #endregion

    #region Scoring functions

    /// <summary>
    /// Adds an item score invoker
    /// </summary>
    /// <param name="script"> pick up script </param>
    public static void AddItemScoreInvoker(Item script)
    {
        item_score_invokers.Add(script);
        foreach (UnityAction<ItemTypes, Vector2> listener in item_score_listeners)
        {
            script.AddItemScoreListener(listener);
        }
    }

    /// <summary>
    ///  Adds scoring Listener
    /// </summary>
    /// <param name="handeler"> event handeler </param>
    public static void AddItemScoreListener(UnityAction<ItemTypes, Vector2> handeler)
    {
        item_score_listeners.Add(handeler);
        foreach (Item item in item_score_invokers)
        {
            item.AddItemScoreListener(handeler);
        }
    }
    /// <summary>
    /// Adds a ninja score invoker
    /// </summary>
    /// <param name="script"> pick up script </param>
    public static void AddNinjaScoreInvoker(Ninja script)
    {
        ninja_score_invokers.Add(script);
        foreach (UnityAction<string, Vector2> listener in ninja_score_listeners)
        {
            script.AddNinjaScoreListener(listener);
        }
    }

    /// <summary>
    ///  Adds scoring Listener
    /// </summary>
    /// <param name="handeler"> event handeler </param>
    public static void AddNinjaScoreListener(UnityAction<string, Vector2> handeler)
    {
        ninja_score_listeners.Add(handeler);
        foreach (Ninja ninja in ninja_score_invokers)
        {
            ninja.AddNinjaScoreListener(handeler);
        }
    }

    #endregion

    #region HighScore methods

    /// <summary>
    /// Adds a new high score invoker
    /// </summary>
    /// <param name="script"> pick up script </param>
    public static void AddHighScoreInvoker(AddHighScoreMenu script)
    {
        highScoreInvokers.Add(script);
        foreach (UnityAction<string> listener in highScoreListeners)
        {
            script.AddHighScoreListener(listener);
        }
    }

    /// <summary>
    ///  Adds scoring Listener
    /// </summary>
    /// <param name="handeler"> event handeler </param>
    public static void AddHighScoreListener(UnityAction<string> handeler)
    {
        highScoreListeners.Add(handeler);
        foreach (AddHighScoreMenu menu in highScoreInvokers)
        {
            menu.AddHighScoreListener(handeler);
        }
    }
    #endregion

}

