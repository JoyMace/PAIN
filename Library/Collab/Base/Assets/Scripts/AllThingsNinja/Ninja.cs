using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ninja : MonoBehaviour
{
    GameObject pirate;

    [SerializeField]
    GameObject Explosion;

    DifficultyLevels difficulty;
       
    // projectile information
    [SerializeField]
    GameObject projectile;
    int projectile_count = NinjaConfiguration.ShurikenCount;
    bool reloaded = true;
    Timer throw_timer;
    
    // greedy ninja will make ninja spawner to choose ninja brain a little further in
    Brain brain;

    //use timer event to move every .05 seconds rather than every frame
    Timer move_timer;
    bool moving = true;

    NinjaRespawnEvent ninjaRespawn = new NinjaRespawnEvent();
    AddNinjaScoreEvent ninjaScoreEvent = new AddNinjaScoreEvent();
    

    // Start is called before the first frame update
    void Start()
    {
        difficulty = Configuration.Difficulty;
        brain_selector();
        startMoveTimer();
        EventManager.AddTResapwnInvoker(this);
        EventManager.AddNinjaScoreInvoker(this);
        
        
    }

    public virtual  void brain_selector()
    {
        switch(difficulty)
        { 
            case DifficultyLevels.Easy:
                brain = gameObject.AddComponent<Greedy>();
                break;

            case DifficultyLevels.Medium:
                brain = gameObject.AddComponent<ReflexBrain>();
                break;
            case DifficultyLevels.Hard:
                brain = gameObject.AddComponent<ReflexBrain>();
                break;
        }
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



    private void getLOS()
    {
        // pirate's position in game
        Vector2 goal;
        Vector2 position;
        pirate = GameObject.Find("pirate_idle_0");

        if (pirate != null)
        {
            goal = pirate.GetComponent<Rigidbody2D>().position;
            position = gameObject.GetComponent<Rigidbody2D>().position;
            goal = goal - position;
            List<RaycastHit2D> results = new List<RaycastHit2D>();
            // Line of sigh to pirate
            RaycastHit2D sighttest = Physics2D.Raycast(transform.position, goal, 10f);

            Debug.DrawRay(transform.position, goal);

            if (sighttest && sighttest.collider.CompareTag("ARR"))
            {
                //print("SEE YOU!!!");
                //Debug.Log(sighttest.collider.tag);
                Vector2 starPosition = transform.position;
                //print("THROWING");
                throw_star(starPosition);
            }
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
            var boom = Instantiate(Explosion, transform.position, Quaternion.identity);            
            ninjaScoreEvent.Invoke("bottle", transform.position);   
            Ninja_poof();            
        }

        if (collision.gameObject.CompareTag("ARR") && collision.gameObject.GetComponent<Pirate>().Drunk)
        {
            var boom = Instantiate(Explosion, transform.position, Quaternion.identity);
            ninjaScoreEvent.Invoke("drunk", transform.position);
            
            Ninja_poof();            
        }
    }

    void throw_star(Vector2 position)
    { 
        //print("pew pew Im a ninja!");
        var star = Instantiate(projectile, position , Quaternion.identity);
        Physics2D.IgnoreCollision(star.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        projectile_count -= 1;
        if(projectile_count == 0)
        {
            Ninja_poof();
        }
        reloaded = false;
        StartThrowTimer();
        AudioManager.Play(AudioClipName.ShurikenThrow);
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

    public void AddNinjaScoreListener(UnityAction<string, Vector2> listener)
    {
        ninjaScoreEvent.AddListener(listener);
    }

}
