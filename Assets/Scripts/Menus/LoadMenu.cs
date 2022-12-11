using UnityEngine;

public class LoadMenu : SaveLoadMenu
{
    public void Load(Transform button)
    {
        LoadSceneManager.LoadScene("Game", button.GetSiblingIndex());
    }


    public void Back()
    {
        PauseMenu.LoadMenu();
    }
}
