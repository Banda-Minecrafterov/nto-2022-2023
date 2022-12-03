using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    public enum SaveObjectId
    {
        test = 0,
        invSlot0, invSlot35 = invSlot0 + 35,
        openWhenInteracted0,
        size,
    }

    static public ISaveLoadData[] saveObjects { get; private set; } = new ISaveLoadData[(int)SaveObjectId.size];


    public static void AddObject(SaveObjectId id, ISaveLoadData data)
    {
        saveObjects[(int)id] = data;
    }


    public static void SaveAll(ref BinaryWriter data, Int32 version)
    {
        data.Write(version);

        foreach (var i in saveObjects)
        {
            i.Save(ref data);
        }
    }

    public static void LoadAll(ref BinaryReader data, Int32 version)
    {
        foreach (var i in saveObjects)
        {
            i.Load(ref data, version);
        }
    }
}