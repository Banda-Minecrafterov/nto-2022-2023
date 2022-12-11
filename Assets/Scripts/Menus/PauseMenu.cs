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
    GameObject keysMenu;
    [SerializeField]
    GameObject beastsMenu;
    [SerializeField]
    GameObject inventoryMenu;

    [SerializeField]
    SaveMenu    saveMenuLocal;
    [SerializeField]
    GameObject  loadMenuLocal;
    [SerializeField]
    UpgradeMenu upgradeMenuLocal;

    [SerializeField]
    GameObject foreground;

    [SerializeField]
    Transform buttons;

    static PauseMenu menu;

    public static SaveMenu    saveMenu    { get => menu.saveMenuLocal;  }
    public static GameObject  loadMenu    { get => menu.loadMenuLocal; }
    public static UpgradeMenu upgradeMenu { get => menu.upgradeMenuLocal; }


    void Awake()
    {
        menu = this;

        StartCoroutine(UpdateMenu());

        beastsMenu.SetActive(true);
        inventoryMenu.SetActive(true);

        upgradeMenu.gameObject.SetActive(true);

#if DEBUG
        settingsMenu.SetActive(false);
        keysMenu.SetActive(false);

        saveMenu.gameObject.SetActive(false);
        loadMenu.SetActive(false);

        foreground.SetActive(false);
#endif
    }


    IEnumerator UpdateMenu()
    {
        while (true)
        {
            if (EnemyChase.isNotInCombat)
            {
                if (Input.GetButtonDown("Beasts"))
                {
                    if (!isPaused)
                    {
                        Pause();
                        foreground.SetActive(true);
                    }

                    BeastsMenu();
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                else if (Input.GetButtonDown("Inventory"))
                {
                    if (!isPaused)
                    {
                        Pause();
                        foreground.SetActive(true);
                    }

                    InventoryMenu();
                    yield return new WaitForSecondsRealtime(0.1f);
                }
                else if (Input.GetButtonDown("Cancel"))
                {
                    if (isPaused)
                    {
                        Pause();
                        foreground.SetActive(false);

                        settingsMenu.SetActive(false);
                        keysMenu.SetActive(false);
                        beastsMenu.SetActive(false);
                        inventoryMenu.SetActive(false);

                        saveMenuLocal.gameObject.SetActive(false);
                        loadMenuLocal.SetActive(false);
                        upgradeMenuLocal.gameObject.SetActive(false);

                        TipManager.TooltipDisable();
                    }
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
            yield return null;
        }
    }


    public void Menu()
    {
        isPaused = false;
        Time.timeScale = 1;

        LoadSceneManager.LoadScene("Menu");
    }


    public void SettingsMenu()
    {
        buttons.GetChild(0).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(true);
        keysMenu.SetActive(false);
        beastsMenu.SetActive(false);
        inventoryMenu.SetActive(false);

        AudioManager.uiOpen.Play();
    }

    public void BeastsMenu()
    {
        buttons.GetChild(1).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(false);
        keysMenu.SetActive(false);
        beastsMenu.SetActive(true);
        inventoryMenu.SetActive(false);

        AudioManager.uiOpen.Play();
    }

    public void InventoryMenu()
    {
        buttons.GetChild(2).gameObject.GetComponent<Button>().Select();

        settingsMenu.SetActive(false);
        keysMenu.SetActive(false);
        beastsMenu.SetActive(false);
        inventoryMenu.SetActive(true);

        AudioManager.uiOpen.Play();
    }


    public static void OpenSaveMenu(Idol idol)
    {
        OpenMenu(saveMenu.gameObject);
        saveMenu.Open(idol);
    }

    public static void CloseSaveMenu()
    {
        OpenMenu(saveMenu.gameObject);
    }

    public static void LoadMenu()
    {
        OpenMenu(loadMenu);
    }

    public static void UpgradeMenu()
    {
        OpenMenu(upgradeMenu.gameObject);
    }

    static void OpenMenu(GameObject menu)
    {
        PauseMenu.menu.Pause();
        menu.SetActive(isPaused);

        AudioManager.uiOpen.Play();
    }


    void Pause()
    {
        foreach (var i in GetPauseComponents())
        {
            i.enabled = isPaused;
        }
        TipManager.Pause();

        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;
        MouseManager.SetMouseMode(isPaused);
    }


    static List<MonoBehaviour> GetPauseComponents()
    {
        List<MonoBehaviour> components = new List<MonoBehaviour>
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>()
        };

        foreach (var i in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            components.Add(i.GetComponent<Enemy>());
        }

        return components;
    }
}
