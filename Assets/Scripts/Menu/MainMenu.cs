using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : BaseMenu
{
    public void NewGame()
    {
        MouseManager.SetMouseMode(false);

        Manager.LoadScene("Game");
    }

    public void Load()
    {
    }

    public void Settings(GameObject settings)
    {
        base.Settings(gameObject, settings);
    }
}
