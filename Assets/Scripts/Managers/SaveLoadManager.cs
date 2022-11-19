using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public void Save()
    {
        foreach (var i in GetSaveLoadObjects())
        {
            i.Save();
        }
    }

    public void Load()
    {
        foreach (var i in GetSaveLoadObjects())
        {
            i.Load();
        }
    }


    List<ISaveLoadData> GetSaveLoadObjects()
    {
        List<ISaveLoadData> datas = new List<ISaveLoadData>();

        foreach (var i in GameObject.FindGameObjectsWithTag("Save-Load"))
        {
            datas.Add(i.GetComponent<ISaveLoadData>());
        }

        return datas;
    }
}
