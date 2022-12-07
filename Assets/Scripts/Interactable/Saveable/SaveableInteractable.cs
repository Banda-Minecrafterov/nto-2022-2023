using System.IO;
using UnityEngine;

public abstract class SaveableInteractable : Interactable, ISaveLoadData
{
    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Interctable0 + transform.GetSiblingIndex(), this);
    }


    protected void Disable()
    {
        StopButtonCheck();
        GetComponent<Collider2D>().enabled = false;
    }


    public void Load(ref BinaryReader data, int version)
    {
        if (data.ReadBoolean())
        {
            Interact();
        }
    }

    public void Save(ref BinaryWriter data)
    {
        data.Write(!GetComponent<Collider2D>().enabled);
    }
}
