using System.IO;
using UnityEngine;

public class test : MonoBehaviour, ISaveLoadData
{
    public int a;


    void Update()
    {
        transform.position += Vector3.left * 0.01f;
    }


    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.test, this);
    }

    public void Load(ref BinaryReader data, int version)
    {
        a = data.ReadInt32();
    }

    public void Save(ref BinaryWriter data)
    {
        data.Write(a);
    }
}
