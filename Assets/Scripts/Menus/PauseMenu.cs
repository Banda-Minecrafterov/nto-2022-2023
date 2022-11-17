using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PauseMenu : BaseMenu
{
    public static bool isPaused = false;

    [SerializeField]
    GameObject pauseMenu;


    void Awake()
    {
#if DEBUG
        pauseMenu.SetActive(false);
#endif

        StartCoroutine(UpdateMenu());
    }


    IEnumerator UpdateMenu()
    {
        while (true)
        {
            if (Input.GetAxisRaw("Cancel") != 0)
            {
                Resume();
                yield return new WaitForSecondsRealtime(0.2f);
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

    public void Save()
    {
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

            components.Add(player.GetComponent<Player>());
            components.Add(player.transform.parent.GetComponentInChildren<CinemachineFreeLook>());
        }

        return components;
    }
}
