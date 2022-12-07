using System;
using System.IO;

public class SaveLoadManager
{
    public enum SaveObjectId
    {
        InvSlot0 = 0, InvSlot26 = InvSlot0 + 26,
        HandSlot0, HandSlot3 = HandSlot0 + 3,

        Interctable0, Interctable1 = Interctable0 + 1,

        Enemy0,

        Player, PlayerHealth,

        Tutorial,
        UpgradeManager,

        Count,
    }

    static ISaveLoadData[] saveObjects = new ISaveLoadData[(int)SaveObjectId.Count];


    public static void AddObject(SaveObjectId id, ISaveLoadData data)
    {
        saveObjects[(int)id] = data;
    }


    public static void SaveAll(ref BinaryWriter data, Int32 version)
    {
        data.Write(version);

        foreach (var i in saveObjects)
        {
            //Debug.Log(Array.FindIndex(saveObjects, x => x == i));
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
