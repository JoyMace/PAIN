using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

        audioClips.Add(AudioClipName.BottleThrow,
            Resources.Load<AudioClip>("BottleThrow"));
        audioClips.Add(AudioClipName.ChooseNinjaBrain,
             Resources.Load<AudioClip>("ChooseNinjaBrain"));
        audioClips.Add(AudioClipName.DoubloonPickupNoise,
            Resources.Load<AudioClip>("DoubloonPickupNoise"));
        audioClips.Add(AudioClipName.DrinkRum,
            Resources.Load<AudioClip>("DrinkRum"));
        audioClips.Add(AudioClipName.Hiccup,
            Resources.Load<AudioClip>("Hiccup"));
        audioClips.Add(AudioClipName.LemonParty,
            Resources.Load<AudioClip>("LemonParty"));
        audioClips.Add(AudioClipName.MenuButtonClick,
            Resources.Load<AudioClip>("MenuButtonClick"));
        audioClips.Add(AudioClipName.NinjaWin,
            Resources.Load<AudioClip>("NinjaWin"));
        audioClips.Add(AudioClipName.PirateBackgroundMusic,
            Resources.Load<AudioClip>("PirateBackgroundMusic"));
        audioClips.Add(AudioClipName.PirateDeath,
            Resources.Load<AudioClip>("PirateDeath"));
        audioClips.Add(AudioClipName.PirateWin,
            Resources.Load<AudioClip>("PirateWin"));
        audioClips.Add(AudioClipName.ShurikenHit,
            Resources.Load<AudioClip>("ShurikenHit"));
        audioClips.Add(AudioClipName.ShurikenThrow,
            Resources.Load<AudioClip>("ShurikenThrow"));                                
      
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name], 0.5f);
    }
}
