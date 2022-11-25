using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : SaveLoadData
{
    public int a;


    void Update()
    {
        transform.position += Vector3.left * 0.01f;
    }


    public override void Awake()
    {
        AddObject(SaveObjectId.test);
    }

    public override void Load(ref BinaryReader data, int version)
    {
        a = data.ReadInt32();
    }

    public override void Save(ref BinaryWriter data)
    {
        data.Write(a);
    }
}
