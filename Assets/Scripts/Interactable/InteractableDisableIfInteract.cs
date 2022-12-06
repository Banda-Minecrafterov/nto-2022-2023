using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class InteractableDisableIfInteract : Interactable, ISaveLoadData
{
    [SerializeField]
    int id;


    void Awake()
    {
        SaveLoadManager.AddObject(GetSaveObjectId() + id, this);
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


    protected abstract SaveLoadManager.SaveObjectId GetSaveObjectId();
}
