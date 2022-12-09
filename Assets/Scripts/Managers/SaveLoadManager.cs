using System;
using System.IO;
using UnityEngine;

public class SaveLoadManager
{
    public static int saveSlotsCount = 3;


    public enum SaveObjectId
    {
        InvSlot0 = 0, InvSlot26 = InvSlot0 + 26,
        HandSlot0, HandSlot3 = HandSlot0 + 3,

        Interctable0, Interctable2 = Interctable0 + 2,

        Enemy0,

        Player, PlayerHealth,

        Tutorial,
        UpgradeManager,

        Count,
    }

    static ISaveLoadData[] saveObjects = new ISaveLoadData[(int)SaveObjectId.Count];

    public static SaveLoadData[] saveLoadDatas { get; private set; } = new SaveLoadData[saveSlotsCount];


    public static void AddObject(SaveObjectId id, ISaveLoadData data)
    {
        saveObjects[(int)id] = data;
    }


    public static void LoadFiles()
    {
        if (saveLoadDatas[0] != null)
            return;

        for (int i = 0; i < saveSlotsCount; i++)
        {
            saveLoadDatas[i] = new SaveLoadData(i);
        }
    }


    public static void Save(int id)
    {
        if (saveLoadDatas[id].PrepareSave(id))
        {
            saveLoadDatas[id].writer.Write(saveLoadDatas[id].version);

            foreach (var i in saveObjects)
            {
                i.Save(saveLoadDatas[id].writer);
            }
        }
    }

    public static void Load(int id)
    {
        if (saveLoadDatas[id].PrepareLoad())
        {
            foreach (var i in saveObjects)
            {
                i.Load(saveLoadDatas[id].reader, saveLoadDatas[id].version);
            }
        }
    }
}
