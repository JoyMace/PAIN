using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    #region Fields
    // Reference to Item rigid body
    public Rigidbody2D rb2d;

    // for projectiles
    public Vector2 direction;

    // Item Type is able to be set from Inspector
    [SerializeField]
    protected ItemTypes itemType;

    // Sound Name to be played on pickup
    [SerializeField]
    protected AudioClipName soundName;

    [SerializeField]
    protected float gold;

    // this string allows for access to game object via name whether it is a clone or not
    // the cloned objects are messing up access so this public string is important as it doesn't change 
    // even if the object is cloned
    // we could instead set a Property that is accessible, but this string name method works and the property one
    // didn't work when I first tried it
    public string item_type;

    // Events Invoked by this object
    public AddItemScoreEvent itemScoreEvent = new AddItemScoreEvent();
    public PickUpEvent pickUp = new PickUpEvent();

    private float health;    
    private float points;
    


    #endregion

    #region Properties
    /// <summary>
    /// The amount of health points added by this Item
    /// Can be a negative value
    /// </summary>
    public float Health
    {
        get { return health; }

        set { health = value; }
    }

    /// <summary>
    /// The number of Points this Item is worth (can be negative)
    /// </summary>
    public float Points
    {
        get { return points; }

        set { points = value; }
    }

    /// <summary>
    /// Name of Audio clip to be played on pickup
    /// </summary>
    public AudioClipName SoundName
    {
        get { return soundName; }

        set { soundName = value; }
    }

    /// <summary>
    /// Value in Gold of an item. Only Doubloons have gold value 
    /// </summary>
    public float Gold
    {
        get { return gold; }

        set { gold = value; }
    }

    
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Event invokers       
        EventManager.AddItemScoreInvoker(this);
        EventManager.AddPickUpInvoker(this);               

        // saves reference to rigid body component
        rb2d = GetComponent<Rigidbody2D>();               
    }

    /// <summary>
    /// Polymorphic way of playing item sounds
    /// </summary>
    /// <param name="soundName"></param>
    public virtual void PlaySound(AudioClipName soundName)
    {
        AudioManager.Play(soundName);
    }


    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ARR"))
        {
            itemScoreEvent.Invoke(itemType, transform.position);
            pickUp.Invoke(this);
            Destroy(gameObject);
        }
    }
        
    /// <summary>
    /// Add Listener for score events
    /// </summary>
    /// <param name="listener"></param>
    public virtual void AddItemScoreListener(UnityAction<ItemTypes, Vector2> listener)
    {
        itemScoreEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds Listener for PickUp Event
    /// Item invokes, ItemSpawner and Pirate listen
    /// </summary>
    /// <param name="listener"></param>
    public void AddPickUpListener(UnityAction<Item> listener)
    {
        pickUp.AddListener(listener);
    }
}
