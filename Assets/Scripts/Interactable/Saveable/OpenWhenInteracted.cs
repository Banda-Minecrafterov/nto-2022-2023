using System.Collections;
using System.IO;
using UnityEngine;

public class OpenWhenInteracted : SaveableInteractable
{
    protected override void Interact()
    {
        StopButtonCheck();
    }

    public new void StopInteracting()
    {
        gameObject.SetActive(false);
    }


    public override void Load(BinaryReader data, int version)
    {
        LoadProtected(data, version);
    }

    public override void Save(BinaryWriter data)
    {
        data.Write(gameObject.activeSelf);
    }


    protected bool LoadProtected(BinaryReader data, int version)
    {
        bool active = data.ReadBoolean();
        gameObject.SetActive(active);
        return active;
    }
}
