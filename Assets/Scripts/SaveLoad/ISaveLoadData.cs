using System;
using System.IO;

public interface ISaveLoadData
{
    public abstract void Save(ref BinaryWriter data);

    public abstract void Load(ref BinaryReader data, Int32 version);
}
