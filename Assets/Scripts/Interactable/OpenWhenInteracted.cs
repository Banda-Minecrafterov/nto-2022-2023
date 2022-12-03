using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OpenWhenInteracted : Interactable, ISaveLoadData
{
    [SerializeField]
    GameObject open;

    [SerializeField]
    int id;


    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.openWhenInteracted0 + id, this);
    }


    protected override void Interact()
    {
        open.SetActive(false);

        StopButtonCheck();

        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }


    public void Load(ref BinaryReader data, int version)
    {
        if (!data.ReadBoolean())
        {
            Interact();
        }
    }

    public void Save(ref BinaryWriter data)
    {
        data.Write(open.activeSelf);
    }
}
