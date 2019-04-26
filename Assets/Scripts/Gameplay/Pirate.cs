using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The class for player controlled pirate
/// </summary>

public class Pirate : MonoBehaviour
{
    
    /// <summary>
    /// The Pirate's Health
    /// </summary>
    [SerializeField]
    private Stat HP;

    /// <summary>
    /// The Pirate's Initial health
    /// </summary>
    [SerializeField]
    private float initHp = 10;

    /// <summary>
    /// The Pirate's Booty
    /// </summary>
    [SerializeField]
    private Stat doubloons;    

    /// <summary>
    /// The Pirate's Initial wallet weight
    /// </summary>
    [SerializeField]
    private float initDoubloons = 0;

    /// <summary>
    /// Max amount of doubloons on the map
    /// </summary>
    [SerializeField]
    private float maxDoubloons;

    [SerializeField]
    private Stat scurvy;

    [SerializeField]
    private float initScurvyTime = 30;

    // existential pirate facts
    Rigidbody2D pirate;
    Timer scurvy_timer;

   
    // projectile things
    [SerializeField]
    GameObject projectile;
    int bottle_count = 0;
       

    // game over support
    GameOverEvent gameOverEvent = new GameOverEvent();

    // pick up support
    PickUpEvent pickUp = new PickUpEvent();

    GameObject music;

    // Drunken Pirate support
    bool drunk = false;
    Timer drunk_timer;

    int minutes;
    int seconds;

    [SerializeField]
    GameObject bubbles;

    #region Properties
    public bool Drunk
    {
        get { return drunk; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Stats
        HP.Initialize(initHp, initHp);
        doubloons.Initialize(initDoubloons, maxDoubloons);
        scurvy.Initialize(initScurvyTime, initScurvyTime);
        //score.Initialize(initScore, initScore);

        // Drunken Pirate support
        drunk_timer = gameObject.AddComponent<Timer>();
        drunk_timer.Duration = 5;
        drunk_timer.timerFinishedEventListener(SoberUp);
                
        // Scurvy support
        scurvy_timer = gameObject.AddComponent<Timer>();
        scurvy_timer.timerFinishedEventListener(pirate_death);
        scurvy_timer.Duration = 30;
        scurvy_timer.Run();

        pirate = GetComponent<Rigidbody2D>();

        // Event Support
        EventManager.AddGameOverInvoker(this);
        EventManager.AddPickUpListener(evaluate_item);
        
        //Background music object
        music = GameObject.FindGameObjectWithTag("BackgroundMusic");

    }

    // Update is called once per frame
    void Update()
    {
        // Scurvy Timer Bar Updates
        seconds = Mathf.FloorToInt(scurvy_timer.ElapsedSeconds - minutes * 60);        
        scurvy.MyCurrentValue = 30 - seconds;        
        if (HP.MyCurrentValue == 0)
        {
            gameOverEvent.Invoke("ninja");
            AudioManager.Play(AudioClipName.NinjaWin);
            Destroy(gameObject);
            Destroy(music);
        }

        if (doubloons.MyCurrentValue == doubloons.MaxValue)
        {
            GameObject chest = GameObject.Find("chest_0");
            chest.GetComponent<Chest>().setComplete(true);
        }
    }

    private void FixedUpdate()
    {
        // move based on input
        Vector3 position = transform.position;
        Vector3 newPosition;
        float throwBottle;
        float verticalInput = 0;
        float horizontalInput = 0;

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        throwBottle = Input.GetAxis("Fire1");

        if (verticalInput != 0 || horizontalInput !=0)
        {
            newPosition = pirate.position;
            newPosition.y += verticalInput * 5 * Time.deltaTime;
            newPosition.x += horizontalInput * 5 * Time.deltaTime;
            pirate.MovePosition(newPosition);
        }

        if(throwBottle !=0 )
        {
            print("Throwing a bottle!");           
            throw_bottle(transform.position);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {        
        if (coll.gameObject.CompareTag("Chest"))
        {
            if(Chest.levelCompleted)
            {
                gameOverEvent.Invoke("pirate");
                AudioManager.Play(AudioClipName.PirateWin);
                Destroy(music);
            }            
        }
    }

    /// <summary>
    /// Evaluates Item to apply effects of Item to Pirate
    /// </summary>
    /// <param name="item"></param>
    void evaluate_item(Item item)
    {
        item.PlaySound(item.SoundName);
        doubloons.MyCurrentValue += item.Gold;
        HP.MyCurrentValue += item.Health;
        //print("EVALUATING ");
        //print(name);
        if (item.item_type == "lemon")
        {           
            scurvy_timer.ElapsedSeconds = 0;
            scurvy.MyCurrentValue = 30;
        }
        else if (item.item_type == "rum")
        {
            if (HP.MyCurrentValue == HP.MaxValue)
            {
                drunk = true;
                drunk_timer.Run();
                gameObject.GetComponent<ParticleSystem>().Play();// plays the bubbles  
            }     
            bottle_count += 1;
        }
              
    }

        
    /// <summary>
    /// Pirate has DIED
    /// </summary>
    void pirate_death()
    {
        Destroy(music);
        HP.MyCurrentValue = 0;
        gameOverEvent.Invoke("ninja");
        AudioManager.Play(AudioClipName.PirateDeath);        
        Destroy(gameObject);
        AudioManager.Play(AudioClipName.NinjaWin);
    }

    /// <summary>
    /// Pirates throw bottles at Ninjas after they have drunk a bottle of Rum
    /// </summary>
    /// <param name="position"></param>
    private void throw_bottle(Vector2 position)
    {
        if (bottle_count > 0)
        {
            AudioManager.Play(AudioClipName.BottleThrow);
            var bottle = Instantiate(projectile, position, Quaternion.identity);
            Physics2D.IgnoreCollision(bottle.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bottle_count -= 1;            
        }
    }

    /// <summary>
    /// A Pirate Sobers up after 5 seconds and is no longer invincible
    /// </summary>
    void SoberUp()
    {
        AudioManager.Play(AudioClipName.Hiccup);
        drunk = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
        //print("WHAT A NIGHT");
        drunk_timer.Stop();
    }
   

    /// <summary>
    /// Adds listener for Game Over event
    /// Pirate Invokes, GameplayManager listens
    /// </summary>
    /// <param name="listener"></param>
    public void AddGameOverListener(UnityAction<String> listener)
    {
        gameOverEvent.AddListener(listener);
    }
   
}
