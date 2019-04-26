using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ninja : MonoBehaviour
{
    GameObject pirate;

    private Animator animator;
    private Transform target;

    // projectile information
    [SerializeField]
    GameObject projectile;
    int projectile_count = 3;
    bool reloaded = true;
    Timer throw_timer;

    //difficulty levels
    [SerializeField]
    string difficulty_level;

    // greedy ninja will make ninja spawner to choose ninja brain a little further in
    Brain brain;

    //use timer event to move every .05 seconds rather than every frame
    Timer move_timer;
    bool moving = true;

    NinjaRespawnEvent ninjaRespawn = new NinjaRespawnEvent();

    // Start is called before the first frame update
    void Start()
    {
        brain_selector();
        startMoveTimer();
        EventManager.AddTResapwnInvoker(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (reloaded == true)
        {
            getLOS();
        }
        if (moving == true)
        {
            transform.position = brain.getNext();
            moving = false;
            startMoveTimer();
        }
    }

    private void brain_selector()
    {
        if (difficulty_level == "EASY")
        {
            brain = gameObject.AddComponent<Greedy>();
        }
        if (difficulty_level == "MEDIUM")
        {
            brain = gameObject.AddComponent<ReflexBrain>();
        }
        if (difficulty_level == "HARD")
        {
            animator = gameObject.GetComponent<Animator>();
            target = GameObject.Find("pirate_idle_0").transform;
        }
    }

    private void getLOS()
    {
        // pirate's position in game
        pirate = GameObject.Find("pirate_idle_0");
        Vector2 goal = pirate.GetComponent<Rigidbody2D>().position;
        Vector2 position =  gameObject.GetComponent<Rigidbody2D>().position;
        goal = goal - position;
        List<RaycastHit2D> results = new List<RaycastHit2D>();

        // Line of sigh to pirate
        RaycastHit2D sighttest = Physics2D.Raycast(transform.position, goal, 10f);

        Debug.DrawRay(transform.position, goal);

        if (sighttest && sighttest.collider.CompareTag("ARR"))
        {
            print("SEE YOU!!!");
            //Debug.Log(sighttest.collider.tag);
            Vector2 starPosition = transform.position;
            print("THROWING");
            throw_star(starPosition);
        }
    }
    void StartThrowTimer()
    {
        throw_timer = gameObject.AddComponent<Timer>();
        throw_timer.Duration = 3f;
        throw_timer.Run();
        throw_timer.timerFinishedEventListener(setReloaded);
    }
    void startMoveTimer()
    {
        move_timer = gameObject.AddComponent<Timer>();
        move_timer.Duration = 0.05f;
        move_timer.Run();
        move_timer.timerFinishedEventListener(setMoving);
    }

    void setReloaded()
    {
        reloaded = true;
        Destroy(throw_timer);
    }
    void setMoving()
    {
        moving = true;
        Destroy(move_timer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bottle"))
        {
            Ninja_poof();
        }
    }

    void throw_star(Vector2 position)
    { 
        print("pew pew Im a ninja!");
        var star = Instantiate(projectile, position , Quaternion.identity);
        Physics2D.IgnoreCollision(star.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        projectile_count -= 1;
        if(projectile_count == 0)
        {
            Ninja_poof();
        }
        reloaded = false;
        StartThrowTimer();
    }

    private void Ninja_poof()
    {
        ninjaRespawn.Invoke();
        Destroy(gameObject);
    }


    public void RespawnListener(UnityAction listener)
    {
        ninjaRespawn.AddListener(listener);
    }
}
