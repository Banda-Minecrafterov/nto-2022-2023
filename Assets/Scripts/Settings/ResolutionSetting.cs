using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ResolutionSetting : MonoBehaviour
{
    int count = 0;

    [SerializeField]
    CustomResolution[] resolutions = new CustomResolution[2];
    CustomResolution currentResolution = new CustomResolution();

    [SerializeField]
    Button next;
    [SerializeField]
    Button prev;

    Image image;


    void Awake()
    {
        image = GetComponent<Image>();

        currentResolution.width  = Display.main.systemWidth;
        currentResolution.height = Display.main.systemHeight;
        resolutions = resolutions.Where(resolution => resolution.width <= currentResolution.width && resolution.height <= currentResolution.height).ToArray();

        count = Array.FindIndex(resolutions, resolution => resolution.width == Screen.width && resolution.height == Screen.height);
        if (count == -1)
        {
            count = 0;
            SetResolution();
        }
        else
        {
            SetImage();
        }

        if (count == 0)
            prev.interactable = false;
        else if (count == resolutions.Length - 1)
            next.interactable = false;
    }


    public void Next()
    {
        count++;

        SetResolution();
        if (count == resolutions.Length - 1)
        {
            next.interactable = false;
        }
        else
        {
            prev.interactable = true;
        }
    }

    public void Prev()
    {
        count--;

        SetResolution();
        if (count == 0)
        {
            prev.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
    }


    void SetResolution()
    {
        SetImage();
        Screen.SetResolution(resolutions[count].width, resolutions[count].height, resolutions[count].width == currentResolution.width && resolutions[count].height == currentResolution.height);
    }

    void SetImage()
    {
        image.sprite = resolutions[count].sprite;
    }
}

