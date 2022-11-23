using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadMenu : SaveLoadMenu
{
    public void Load(int id)
    {
        switch (datas[id].version)
        {
            case Data.ErrorOcured:
                // TODO
                id = SaveSlotCount;
                goto default;

            case Data.FileNotFound:
                id = SaveSlotCount;
                goto default;

            default:
                for (int i = 0; i < id; i++)
                {
                    Destroy(datas[i]);
                }
                for (int i = id + 1; i < SaveSlotCount; i++)
                {
                    Destroy(datas[i]);
                }

                MouseManager.SetMouseMode(false);
                Manager.LoadScene("Game");
                break;
        }
    }


    public override Data GetData(int i)
    {
        return Manager.LoadSaveFile(i);
    }
}
