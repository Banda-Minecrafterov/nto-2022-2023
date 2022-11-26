using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu
{
    public static bool isPaused { get; private set; } = false;

    [SerializeField]
    GameObject pauseMenu;
    [SerializeField]
    GameObject settingsMenu;

    [SerializeField]
    GameObject saveMenu;

    static PauseMenu menu;
    

    void Awake()
    {
        menu = this;

        StartCoroutine(UpdateMenu());

#if DEBUG
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        saveMenu.SetActive(false);
#endif
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

        if (!isPaused)
            settingsMenu.SetActive(false);
        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
        MouseManager.SetMouseMode(isPaused);
    }

    public void Settings(GameObject settings)
    {
        Settings(pauseMenu, settings);
    }

    public void Menu()
    {
        isPaused = false;
        Time.timeScale = 1;

        Manager.LoadScene("Menu");
    }


    public static void SaveMenu()
    {
        menu.Resume();
        if (isPaused)
        {
            menu.pauseMenu.SetActive(false);
        }
        menu.saveMenu.SetActive(isPaused);
    }


    static List<MonoBehaviour> GetPauseComponents()
    {
        List<MonoBehaviour> components = new List<MonoBehaviour>();

        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            components.Add(player.GetComponent<test>());
        }

        return components;
    }
}
