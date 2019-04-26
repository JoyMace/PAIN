using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public string item_name;
    public PickUpEvent pickUpEvent = new PickUpEvent();

    public Rigidbody2D rb2d;

    // for projectiles
    public Vector2 direction;


    // Start is called before the first frame update
    public virtual void Start()
    {
        setName();
        EventManager.AddPickUpInvoker(this);
    }

    public virtual void setName()
    {
        item_name = "item";
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ARR"))
        {
            pickUpEvent.Invoke(name);
            Destroy(gameObject);
        }
    }

    public virtual void PickUpListener(UnityAction<string> listener)
    {
        pickUpEvent.AddListener(listener);
    }
}
