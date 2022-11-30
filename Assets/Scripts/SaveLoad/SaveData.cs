using System;
using System.IO;
using UnityEngine;

public class SaveData : Data
{
    BinaryWriter writer;


    void OnDestroy()
    {
        writer?.Close();
        reader?.Close();
        fs?.Close();
    }


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
            writer = new BinaryWriter(fs);
        }
        else
        {
            fs.SetLength(0);
        }
        version = Convert.ToInt32(Application.version);

        SaveLoadData.SaveAll(ref writer, version);
    }
}
