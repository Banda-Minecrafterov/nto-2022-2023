using UnityEngine;

public class MainMenu : BaseMenu
{
    void Awake()
    {
        SaveLoadManager.LoadFiles();
    }


    public void Settings(GameObject settings)
    {
        Settings(gameObject, settings);
    }
}
