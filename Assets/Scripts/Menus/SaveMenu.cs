using UnityEngine;

public class SaveMenu : SaveLoadMenu
{
    Idol idol;


    public void Save(Transform button)
    {
        int id = button.GetSiblingIndex();

        SaveLoadManager.Save(id);
        idol.isSaveable = false;

        InitButton(button, id);

        PauseMenu.CloseSaveMenu();
    }


    public void Open(Idol idol)
    {
        this.idol = idol;
    }
}
