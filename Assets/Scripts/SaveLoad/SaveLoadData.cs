using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class SaveLoadData : MonoBehaviour
{
    public enum SaveObjectId
    {
        test = 0, size
    }

    static public SaveLoadData[] saveObjects { get; private set; } = new SaveLoadData[(int)SaveObjectId.size];


    public void AddObject(SaveObjectId id)
    {
        saveObjects[(int)id] = this;
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


    public abstract void Awake();

    public abstract void Save(ref BinaryWriter data);

    public abstract void Load(ref BinaryReader data, Int32 version);
}
