using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Idol : SaveableInteractable
{
    public bool isSaveable = true;


    protected override void Interact()
    {
        PauseMenu.UpgradeMenu(this);
    }


    public override void Load(BinaryReader data, int version)
    {
        isSaveable = data.ReadBoolean();
    }

    public override void Save(BinaryWriter data)
    {
        data.Write(isSaveable);
    }
}
