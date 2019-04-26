using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ninja : MonoBehaviour
{
    // greedy ninja will make ninja spawner to choose ninja brain a little further in
    Greedy brain;

    //use timer event to move every .05 seconds rather than every frame
    Timer move_timer;
    bool moving = false;

    PickUpEvent pickUp = new PickUpEvent();

    // Start is called before the first frame update
    void Start()
    {
        brain = gameObject.AddComponent<Greedy>();
        startMoveTimer();
        EventManager.AddPickUpListener(evaluate_pickUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            transform.position = brain.getNext();
            moving = false;
            startMoveTimer();
        }
    }
    void startMoveTimer()
    {
        move_timer = gameObject.AddComponent<Timer>();
        move_timer.Duration = 0.05f;
        move_timer.Run();
        move_timer.timerFinishedEventListener(setMoving);
    }

    void setMoving()
    {
        moving = true;
        Destroy(move_timer);
    }

    void evaluate_pickUp(string name)
    {
        if (name == "bottle")
        {
            Destroy(gameObject);
        }
    }

    public void PickUpListener(UnityAction<string> listener)
    {
        pickUp.AddListener(listener);
    }
}
