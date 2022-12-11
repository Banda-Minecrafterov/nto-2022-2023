using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadData
{
    FileStream fs;
    public BinaryReader reader { get; private set; }
    public BinaryWriter writer { get; private set; }


    public const Int32 FileNotFound = Int32.MinValue, ErrorOcured = Int32.MinValue + 1;
    public Int32 version { get; private set; }
    public Exception exception { get; private set; }


    public SaveLoadData(int id)
    {
        try
        {
            fs = File.Open(Path.Combine(Application.persistentDataPath, id + ".sav"), FileMode.Open, FileAccess.ReadWrite);
        }
        catch (FileNotFoundException)
        {
            version = FileNotFound;
            return;
        }
        catch (Exception e)
        {
            exception = e;
            version = ErrorOcured;
            return;
        }

        reader = new BinaryReader(fs);
        writer = new BinaryWriter(fs);

        version = reader.ReadInt32();
    }

    ~SaveLoadData()
    {
        fs?.Close();
        reader?.Close();
        writer?.Close();
    }


    public bool PrepareLoad()
    {
        if (version == FileNotFound || version == ErrorOcured)
            return false;

        fs.Position = sizeof(Int32);
        return true;
    }

    public bool PrepareSave(int id)
    {
        switch (version)
        {
            case FileNotFound:
                fs = File.Open(Path.Combine(Application.persistentDataPath, id + ".sav"), FileMode.Create, FileAccess.ReadWrite);

                reader = new BinaryReader(fs);
                writer = new BinaryWriter(fs);
                break;

            case ErrorOcured:
                return false;

            default:
                fs.SetLength(0);
                break;
        }
        version = Convert.ToInt32(Application.version);

        fs.Position = 0;
        return true;
    }
}
