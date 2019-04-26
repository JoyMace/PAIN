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
    /// The Pirate's Booty
    /// </summary>
    [SerializeField]
    private Stat doubloons;

    /// <summary>
    /// The Pirate's Initial health
    /// </summary>
    [SerializeField]
    private float initHp = 10;

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
    //int scurvy = 30;
    Timer scurvy_timer;
   
    // projectile things
    [SerializeField]
    GameObject projectile;
    int bottle_count = 0;

    // pick up items
    PickUpEvent pickUp = new PickUpEvent();

    // game over support
    GameOverEvent gameOverEvent = new GameOverEvent();

    GameObject music;

    // Drunken Pirate support
    bool drunk = false;
    Timer drunk_timer;

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

        // Drunken Pirate support
        drunk_timer = gameObject.AddComponent<Timer>();
        drunk_timer.Duration = 5;
        drunk_timer.timerFinishedEventListener(SoberUp);

        addScurvyTime();
        pirate = GetComponent<Rigidbody2D>();

        // Event Support
        EventManager.AddPickUpListener(evaluate_item);
        EventManager.AddGameOverInvoker(this);


        //Background music object
        music = GameObject.FindGameObjectWithTag("BackgroundMusic");

    }

    // Update is called once per frame
    void Update()
    {
        scurvy.MyCurrentValue -= scurvy_timer.ElapsedSeconds + 0.1f;
        if (HP.MyCurrentValue <= 0)
        {
            pirate_death();
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Chest"))
        {
            if(Chest.levelCompleted)
            {
                gameOverEvent.Invoke("pirate");
            }
            
        }
    }

    void evaluate_item(ItemTypes name)
    {
        print("EVALUATING ");
        print(name);
        if (name == ItemTypes.lemon)
        {
            restartScurvy();
            scurvy.MyCurrentValue = 30;
        }
        else if (name == ItemTypes.rum)
        {
            if (HP.MyCurrentValue == HP.MaxValue)
            {
                print("I'm DRUNK!");                
                drunk = true;
                drunk_timer.Run();                
                gameObject.GetComponent<ParticleSystem>().Play();// plays the bubbles
            }
            else
            {
                HP.MyCurrentValue += 2;
            }
            bottle_count += 1;
            AudioManager.Play(AudioClipName.DrinkRum);
        }
        else if (name == ItemTypes.doubloon)
        {
            print("Im Rich!");
            doubloons.MyCurrentValue += 1;
            if (doubloons.MyCurrentValue == 20)
            {
                GameObject chest = GameObject.Find("chest_0");
                chest.GetComponent<Chest>().setComplete(true);
            }
            AudioManager.Play(AudioClipName.DoubloonPickupNoise);
        }
        else if (name == ItemTypes.shuriken)
        {
            if (drunk)
            {
                print("I AM INVINCIBLE! ...hic");
            }
            else
            {
                print("OW");
                HP.MyCurrentValue -= 2;
            }
            
        }
    }

    void addScurvyTime()
    {
        pirate = GetComponent<Rigidbody2D>();
        scurvy_timer = gameObject.AddComponent<Timer>();
        scurvy_timer.Duration = initScurvyTime;
        scurvy_timer.Run();        
        scurvy_timer.timerFinishedEventListener(pirate_death);
    }

    void restartScurvy()
    {
        print("LEMON PARTY");
        Destroy(scurvy_timer);
        addScurvyTime();
    }
    
    /// <summary>
    /// Pirate has DIED
    /// </summary>
    void pirate_death()
    {
        HP.MyCurrentValue = 0;
        gameOverEvent.Invoke("ninja");
        AudioManager.Play(AudioClipName.PirateDeath);
        Destroy(music);
        Destroy(gameObject);
    }

    /// <summary>
    /// Pirates throw bottles at Ninjas after they have drunk a bottle of Rum
    /// </summary>
    /// <param name="position"></param>
    private void throw_bottle(Vector2 position)
    {
        if (bottle_count > 0)
        {
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
        drunk = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
        print("WHAT A NIGHT");
    }
    
    /// <summary>
    /// Adds listener for PickUp event
    /// </summary>
    /// <param name="listener"></param>
    public void PickUpListener(UnityAction<ItemTypes> listener)
    {
        pickUp.AddListener(listener);
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
