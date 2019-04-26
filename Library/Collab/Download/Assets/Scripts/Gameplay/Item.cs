using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public PickUpEvent pickUpEvent = new PickUpEvent();

    public Rigidbody2D rb2d;

    // for projectiles
    public Vector2 direction;

    // Item Name
    [SerializeField]
    protected ItemName itemName;

    public string item_name;

    // Start is called before the first frame update
    public virtual void Start()
    {
        EventManager.AddPickUpInvoker(this);
        if (itemName == ItemName.Bottle)
        {
            item_name = "bottle";
        }

        else if (itemName == ItemName.Doubloon)
        {
            item_name = "doubloon";
        }

        else if (itemName == ItemName.Lemon)
        {
            item_name = "lemon";
        }
        else if (itemName == ItemName.Rum)
        {
            item_name = "rum"; 
        }
        else if (itemName == ItemName.Shuriken)
        {
            item_name = "shuriken";
        }

        rb2d = GetComponent<Rigidbody2D>();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ARR"))
        {
            pickUpEvent.Invoke(this.item_name);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Add Listener for PickUp event
    /// </summary>
    /// <param name="listener"></param>
    public virtual void PickUpListener(UnityAction<string> listener)
    {
        pickUpEvent.AddListener(listener);
    }
}
