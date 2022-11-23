using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ISaveLoadData
{
    public abstract void Save(ref BinaryWriter data);

    public abstract void Load(ref BinaryReader data, Int32 version);
}
