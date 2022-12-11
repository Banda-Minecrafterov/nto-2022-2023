using System;
using System.IO;

public interface ISaveLoadData
{
    public void Save(BinaryWriter data);

    public void Load(BinaryReader data, Int32 version);
}
