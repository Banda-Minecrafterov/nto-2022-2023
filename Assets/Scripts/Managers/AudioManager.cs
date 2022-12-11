using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource backgroundLocal;
    [SerializeField]
    AudioSource playerSnowWalkLocal;
    [SerializeField]
    AudioSource playerDashLocal;
    [SerializeField]
    AudioSource uiOpenLocal;

    public static AudioManager manager { get; private set; }

    public static AudioSource background     { get => manager.backgroundLocal; }
    public static AudioSource playerSnowWalk { get => manager.playerSnowWalkLocal; }
    public static AudioSource playerDash     { get => manager.playerDashLocal; }
    public static AudioSource uiOpen         { get => manager.uiOpenLocal; }

    public static float volume { get => PlayerPrefs.GetFloat("Audio volume", 0.5f); }


    void Awake()
    {
        manager = this;
    }


    public static void SetAudioVolume()
    {
        SetAudioVolume(volume);
    }

    public static void SetAudioVolume(float volume)
    {
        background.volume = volume;
        playerSnowWalk.volume = volume;
        playerDash.volume = volume;
        uiOpen.volume = volume;

        foreach (var i in CharacterAttack.audioSources)
        {
            i.volume = volume;
        }

        foreach (var i in IdolActivate.audioSources)
        {
            i.volume = volume;
        }

        foreach (var i in Idol.audioSources)
        {
            i.volume = volume;
        }

        PlayerPrefs.SetFloat("Audio volume", volume);
    }
}
