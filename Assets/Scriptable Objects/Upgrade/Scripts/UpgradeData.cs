using System.IO;
using UnityEngine;


[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    public enum Type
    {
        Attack = 0, Health, //Stamina,

        Count
    }

    public int sunEnergy;
    public CharacterBuff buff;

    public UpgradeData next;


    public void Save(ref BinaryWriter data, UpgradeData current)
    {
        int i = 0;
        for (UpgradeData dataIt = this; dataIt != current; i++)
        {
            dataIt = dataIt.next;
        }
        data.Write(i);
    }

    public void Load(ref BinaryReader data, int version, Type type)
    {
        int count = data.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            UpgradeManager.ForceUpgrade(type);
        }
    }
}
