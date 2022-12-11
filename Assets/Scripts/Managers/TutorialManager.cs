using System.IO;
using UnityEngine;

public class TutorialManager : MonoBehaviour, ISaveLoadData
{
    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Tutorial, this);
    }


    public void Load(BinaryReader data, int version)
    {
        enabled = false;
    }

    public void Save(BinaryWriter data)
    {
    }
}
