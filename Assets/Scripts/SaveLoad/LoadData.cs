using System.IO;

public class LoadData : Data
{
    public void Open(int id)
    {
        Open(id + ".sav", FileMode.Open, FileAccess.Read);
    }

    public void Load()
    {
        if (version == FileNotFound || version == ErrorOcured)
            return;

        SaveLoadManager.LoadAll(ref reader, version);
        reader.Close();

        fs.Close();

        Destroy(this);
    }
}
