using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ninjas go POOF
/// </summary>
public class Explosion : Item
{
    [SerializeField]
    GameObject explosion;

    Timer ExplosionTImer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 0;
        Points = 0;
        itemType = ItemTypes.Explosion;
        item_type = "explosion";
        soundName = AudioClipName.NinjaDeath;
        gold = 0;
        ExplosionTImer = gameObject.AddComponent<Timer>();
        ExplosionTImer.Duration = 1.0f;
        ExplosionTImer.Run();
        ExplosionTImer.timerFinishedEventListener(DestroyExplosion);
        Debug.Log("EXPLODEE");
    }

    /// <summary>
    /// But the poof is not everlasting
    /// </summary>
    void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
