using System;
using System.IO;
using UnityEngine;

public class SaveData : Data
{
    BinaryWriter writer;

    public void Open(int id)
    {
        if (Open(id + ".sav", FileMode.Open, FileAccess.ReadWrite))
            writer = new BinaryWriter(fs);
    }

    public void Save(int id)
    {
        if (version == FileNotFound || version == ErrorOcured)
        {
            fs     = File.Open(Path.Combine(Application.persistentDataPath, id + ".sav"), FileMode.Create, FileAccess.ReadWrite);
            
            reader = new BinaryReader(fs);
            writer = new BinaryWriter(fs);

            version = Convert.ToInt32(Application.version);
        }
        Debug.Log("SaveData Save");

        SaveLoadManager.Save(ref writer, version);

        Destroy(this);
    }
}
