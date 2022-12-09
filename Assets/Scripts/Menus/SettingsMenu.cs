using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    [SerializeField]
    TMP_Dropdown resolutionDropdown;
    [SerializeField]
    AudioSource audioSource;

    Resolution[] resolutions;


    void Awake()
    {
#if DEBUG
        resolutionDropdown.options.Clear();
#endif

        resolutions = Screen.resolutions.Reverse().ToArray();
        foreach (var i in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(i.width + "x" + i.height + " " + i.refreshRate + "Hz"));
        }
    }


    public void Back(GameObject menu)
    {
        Settings(gameObject, menu);
    }
    
    public void ChangeResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width,
            resolutions[resolutionIndex].height,
            resolutionIndex == 0 || (resolutions[resolutionIndex].width == resolutions[0].width && resolutions[resolutionIndex].height == resolutions[0].height),
            resolutions[resolutionIndex].refreshRate);
    }
    
    public void ChangeAudio(float volume)
    {
        audioSource.volume = volume;
    }
}
