using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NinjaSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject easy_ninja;
    [SerializeField]
    GameObject medium_ninja;
    [SerializeField]
    GameObject hard_ninja;

    GameObject ninja;

    DifficultyLevels difficulty;

    List<Vector3> spawnPos = new List<Vector3>();

    int max_ninjas;
    int current_ninjas = 0;
    bool training_complete = true;

    float train_time_min;
    float train_time_max;

    Timer training_timer;
    NinjaRespawnEvent ninjaRespawn = new NinjaRespawnEvent();

    // Start is called before the first frame update
    void Start()
    {
        NinjaConfiguration.CreateSpawnList(spawnPos);
        difficulty = Configuration.Difficulty;
        max_ninjas = NinjaConfiguration.MaxNinjas;
        train_time_min = NinjaConfiguration.MinTraining;
        train_time_max = NinjaConfiguration.MaxTraining;
        
        if (difficulty == DifficultyLevels.Easy)
        {
            ninja = easy_ninja;
        }
        else if (difficulty == DifficultyLevels.Medium)
        {
            ninja = medium_ninja;
        }
        else if (difficulty == DifficultyLevels.Hard)
        {
            ninja = hard_ninja;
        }
        else
        {
            ninja = easy_ninja;
        }
        spawn_ninjas(ninja);
        EventManager.AddRespawnListener(respawn_ninja);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (current_ninjas < max_ninjas)
        {
            spawn_ninjas(ninja);
        }
    }

    /// <summary>
    /// reduces count of ninjas on scene
    /// </summary>
    private void respawn_ninja()
    {
        current_ninjas -= 1;
    }

    /// <summary>
    /// Spawns Ninjas into scene
    /// </summary>
    private void spawn_ninjas(GameObject ninja)
    {
        
        
        if (current_ninjas < max_ninjas)
        {
            if (training_complete == true)
            {
                Vector3 infilatration_point = get_position();
                Instantiate(ninja, infilatration_point, Quaternion.identity);
                current_ninjas += 1;
                training_complete = false;
                start_training();
            }
        }
    }

    void start_training()
    {
        Timer training_timer = gameObject.AddComponent<Timer>();
        training_timer.Duration = Random.Range(train_time_min, train_time_max);
        training_timer.Run();
        training_timer.timerFinishedEventListener(set_trained);

    }

    Vector3 get_position()
    {
        int index = Random.Range(0, 3);
        return spawnPos[index];
    }

    void set_trained()
    {
        training_complete = true;
        spawn_ninjas(ninja);
    }

    public void RespawnListener(UnityAction listener)
    {
        ninjaRespawn.AddListener(listener);
    }
}
