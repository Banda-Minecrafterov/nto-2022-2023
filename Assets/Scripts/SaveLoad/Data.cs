using System;
using System.IO;
using UnityEngine;

public class Data : MonoBehaviour
{
    protected FileStream fs;
    protected BinaryReader reader;

    public const Int32 FileNotFound = Int32.MinValue, ErrorOcured = Int32.MinValue + 1;
    public Int32 version;
    public Exception e;


    protected bool Open(string path, FileMode fileMode, FileAccess fileAccess)
    {
        try
        {
            fs = File.Open(Path.Combine(Application.persistentDataPath, path), fileMode, fileAccess);
        }
        catch (FileNotFoundException)
        {
            version = FileNotFound;
            return false;
        }
        catch (Exception e)
        {
            this.e = e;
            version = ErrorOcured;
            return false;
        }

        reader  = new BinaryReader(fs);
        version = reader.ReadInt32();

        return true;
    }
}
