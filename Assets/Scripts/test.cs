using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour, ISaveLoadData
{
    public int a;


    public void Load(ref BinaryReader data, int version)
    {
        Debug.Log("reading");
        a = data.ReadInt32();
    }

    public void Save(ref BinaryWriter data)
    {
        Debug.Log("wrtitng");
        data.Write(a);
    }
}
