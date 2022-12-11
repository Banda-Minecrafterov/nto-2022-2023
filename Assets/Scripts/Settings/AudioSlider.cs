using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    void OnDisable()
    {
        PlayerPrefs.Save();
    }


    void Awake()
    {
        Slider slider = GetComponent<Slider>();

#if DEBUG
        slider.onValueChanged.RemoveAllListeners();
#endif

        slider.value = AudioManager.volume;
        slider.onValueChanged.AddListener(ChangeAudio);
    }


    public void ChangeAudio(float volume)
    {
        AudioManager.SetAudioVolume(volume);
    }
}
