using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaConfiguration : MonoBehaviour
{
    private static int max_ninjas;
    private static float min_training;
    private static float max_training;
    private static float movement_speed;
    private static int shuriken_count;
    private static int throwing_arm;
    

    public static int MaxNinjas
    {
        get { return max_ninjas; }
        set { max_ninjas = value; }
    }

    public static float MinTraining
    {
        get { return min_training; }
        set { min_training = value; }
    }

    public static float MaxTraining
    {
        get { return max_training; }
        set { max_training = value; }
    }

    public static float MovementSpeed
    {
        get { return movement_speed; }
        set { movement_speed = value; }
    }

    public static int ShurikenCount
    {
        get { return shuriken_count; }
        set { shuriken_count = value; }
    }

    public static int ThrowingArm
    {
        get { return throwing_arm; }
        set { throwing_arm = value; }
    }

    public static void set_Config(DifficultyLevels level)
    {
        switch (level)
        {
            case DifficultyLevels.Easy:
                NinjaConfiguration.MaxNinjas = EasyConfiguration.max_ninjas;
                NinjaConfiguration.MinTraining = EasyConfiguration.min_training;
                NinjaConfiguration.MaxTraining = EasyConfiguration.max_training;
                NinjaConfiguration.MovementSpeed = EasyConfiguration.movement_speed;
                NinjaConfiguration.ShurikenCount = EasyConfiguration.shuriken_count;
                NinjaConfiguration.ThrowingArm = EasyConfiguration.shuriken_count;
                break;
            case DifficultyLevels.Medium:
                NinjaConfiguration.MaxNinjas = MediumCOnfiguration.max_ninjas;
                NinjaConfiguration.MinTraining = MediumCOnfiguration.min_training;
                NinjaConfiguration.MaxTraining = MediumCOnfiguration.max_training;
                NinjaConfiguration.MovementSpeed = MediumCOnfiguration.movement_speed;
                NinjaConfiguration.ShurikenCount = MediumCOnfiguration.shuriken_count;
                NinjaConfiguration.ThrowingArm = MediumCOnfiguration.throwing_arm;
                break;
            case DifficultyLevels.Hard:
                NinjaConfiguration.MaxNinjas = HardConfiguration.max_ninjas;
                NinjaConfiguration.MinTraining = HardConfiguration.min_training;
                NinjaConfiguration.MaxTraining = HardConfiguration.max_training;
                NinjaConfiguration.MovementSpeed = HardConfiguration.movement_speed;
                NinjaConfiguration.ShurikenCount = HardConfiguration.shuriken_count;
                NinjaConfiguration.ThrowingArm = HardConfiguration.throwing_arm;
                break;

        }
    }

    public static void CreateSpawnList(List<Vector3> spawnPos)
    {
        spawnPos.Add(new Vector3(11.5f, 5f, 0));
        spawnPos.Add(new Vector3(-11.5f, 5f, 0));
        spawnPos.Add(new Vector3(11.5f, -5f, 0));
    }
}
