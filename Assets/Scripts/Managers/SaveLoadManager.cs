using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    static List<ISaveLoadData> saveLoadObjects;


    public static void Save(ref BinaryWriter data, Int32 version)
    {
        data.Write(version);

        foreach (var i in saveLoadObjects)
        {
            i.Save(ref data);
        }
    }

    public static void Load(ref BinaryReader data, Int32 version)
    {
        GetSaveLoadObjects();

        foreach (var i in saveLoadObjects)
        {
            i.Load(ref data, version);
        }
    }


    public static void GetSaveLoadObjects()
    {
        saveLoadObjects = new List<ISaveLoadData>();

        foreach (var i in GameObject.FindGameObjectsWithTag("Save Load"))
        {
            saveLoadObjects.Add(i.GetComponent<ISaveLoadData>());
        }
    }
}
