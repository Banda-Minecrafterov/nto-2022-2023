using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public static bool isPaused { get; private set; } = false;

    [SerializeField]
    GameObject settingsMenu;
    [SerializeField]
    GameObject beastsMenu;
    [SerializeField]
    GameObject inventoryMenu;

    [SerializeField]
    GameObject saveMenu;

    [SerializeField]
    GameObject background;

    [SerializeField]
    Transform buttons;

    static PauseMenu menu;
    

    void Awake()
    {
        menu = this;

        StartCoroutine(UpdateMenu());

#if DEBUG
        settingsMenu.SetActive(false);
        beastsMenu.SetActive(false);

        background.SetActive(false);

        saveMenu.SetActive(false);
#endif
    }


    IEnumerator UpdateMenu()
    {
        while (true)
        {
            if (Input.GetButtonDown("Beasts"))
            {
                if (!isPaused)
                    Pause();

                BeastsMenu();
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else if (Input.GetButtonDown("Inventory"))
            {
                if (!isPaused)
                    Pause();

                InventoryMenu();
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                if (isPaused)
                {
                    Pause();

                    menu.settingsMenu.SetActive(false);
                    menu.beastsMenu.SetActive(false);
                    menu.inventoryMenu.SetActive(false);
                }
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return null;
        }
    }


    public void Menu()
    {
        isPaused = false;
        Time.timeScale = 1;

        Manager.LoadScene("Menu");
    }


    public void SettingsMenu()
    {
        buttons.GetChild(0).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(true);
        beastsMenu.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    public void BeastsMenu()
    {
        buttons.GetChild(1).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(false);
        beastsMenu.SetActive(true);
        inventoryMenu.SetActive(false);
    }

    public void InventoryMenu()
    {
        buttons.GetChild(2).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(false);
        beastsMenu.SetActive(false);
        inventoryMenu.SetActive(true);
    }


    public static void SaveMenu()
    {
        menu.Pause();
        menu.saveMenu.SetActive(isPaused);
    }


    void Pause()
    {
        foreach (var i in GetPauseComponents())
        {
            i.enabled = isPaused;
        }

        isPaused = !isPaused;

        background.SetActive(isPaused);

        Time.timeScale = isPaused ? 0 : 1;
        MouseManager.SetMouseMode(isPaused);

        Mathf.Round(0.5f);
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
