using System.IO;
using UnityEngine;

public abstract class SaveableInteractable : Interactable, ISaveLoadData
{
    new void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.Interctable0 + transform.GetSiblingIndex(), this);

        base.Awake();
    }


    protected void Disable()
    {
        StopButtonCheck();
        GetComponent<Collider2D>().enabled = false;
    }


    public virtual void Load(BinaryReader data, int version)
    {
        if (data.ReadBoolean())
        {
            Interact();
        }
    }

    public virtual void Save(BinaryWriter data)
    {
        data.Write(!GetComponent<Collider2D>().enabled);
    }
}
