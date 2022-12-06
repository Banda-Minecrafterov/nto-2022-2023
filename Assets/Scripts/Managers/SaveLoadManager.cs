using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    public enum SaveObjectId
    {
        invSlot0 = 0, invSlot26 = invSlot0 + 26,
        handSlot0, handSlot3 = handSlot0 + 3,
        openWhenInteracted0,
        itemGetter0,
        itemTaker0,
        size,
    }

    static ISaveLoadData[] saveObjects = new ISaveLoadData[(int)SaveObjectId.size];


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
