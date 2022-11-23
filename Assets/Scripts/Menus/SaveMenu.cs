using Unity.VisualScripting;
using UnityEngine;

public class SaveMenu : SaveLoadMenu
{
    void OnEnable()
    {
        MouseManager.SetMouseMode(true);
    }

    void OnDisable()
    {
        MouseManager.SetMouseMode(false);
    }


    public void Save(int id)
    {
        ((SaveData)datas[id]).Save(id);

        InitButton(buttons.GetChild(id), id);

        Back();
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }


    public override Data GetData(int i)
    {
        return Manager.SaveSaveFile(i);
    }
}
