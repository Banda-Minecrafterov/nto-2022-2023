using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu
{
    public static bool isPaused { get; private set; }

    [SerializeField]
    GameObject pauseMenu;

    public static GameObject saveMenu { get; private set; }
    

    void Awake()
    {
#if DEBUG
        pauseMenu.SetActive(false);
#endif

        isPaused = false;
        saveMenu = transform.Find("Save").gameObject;

        StartCoroutine(UpdateMenu());
    }


    IEnumerator UpdateMenu()
    {
        while (true)
        {
            if (Input.GetButtonDown("Cancel") && !saveMenu.activeSelf)
            {
                Resume();
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return null;
        }
    }


    public void Resume()
    {
        foreach (var i in GetPauseComponents())
        {
            i.enabled = isPaused;
        }


        isPaused = !isPaused;

        {
            GameObject settings = pauseMenu.transform.Find("Settings").gameObject;
            if (isPaused && settings.activeSelf)
            {
                pauseMenu.transform.Find("Menu").gameObject.SetActive(true);
                settings.gameObject.SetActive(false);
            }
        }
        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
        MouseManager.SetMouseMode(isPaused);
    }

    public void Settings(GameObject settings)
    {
        base.Settings(pauseMenu.transform.Find("Menu").gameObject, settings);
    }

    public void Menu()
    {
        isPaused = false;
        Time.timeScale = 1;

        Manager.LoadScene("Menu");
    }


    List<MonoBehaviour> GetPauseComponents()
    {
        List<MonoBehaviour> components = new List<MonoBehaviour>();

        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
        }

        return components;
    }
}
