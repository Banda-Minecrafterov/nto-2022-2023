using System.IO;
using UnityEngine;

public class UpgradeManager : MonoBehaviour, ISaveLoadData
{
    [SerializeField]
    UpgradeData[] beginUpgrade = new UpgradeData[(int)UpgradeData.Type.Count];

    public static UpgradeData[] currentUpgrade { get; private set; } = new UpgradeData[(int)UpgradeData.Type.Count];

    public static int sunEnergy { get; private set; } = 120;

    public static UpgradeManager manager { get; private set; }


    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.UpgradeManager, this);

        manager = this;

        beginUpgrade.CopyTo(currentUpgrade, 0);
    }


    public static void AddSunEnergy(int added)
    {
        sunEnergy += added;
    }


    public static UpgradeData Upgrade(UpgradeData.Type type)
    {
        ref UpgradeData data = ref currentUpgrade[(int)type];

        sunEnergy -= data.sunEnergy;
        Player.player.AddPermBuff(data.buff);
        data = data.next;

        return data;
    }

    public static void ForceUpgrade(UpgradeData.Type type)
    {
        ref UpgradeData data = ref currentUpgrade[(int)type];

        Player.player.AddPermBuff(data.buff);
        data = data.next;

        UpgradeMenu.UpdateButton(type, data);
    }


    public void Save(ref BinaryWriter data)
    {
        data.Write(sunEnergy);
        for (int i = 0; i < (int)UpgradeData.Type.Count; i++)
        {
            beginUpgrade[i].Save(ref data, currentUpgrade[i]);
        }
    }

    public void Load(ref BinaryReader data, int version)
    {
        sunEnergy = data.ReadInt32();
        for (int i = 0; i < (int)UpgradeData.Type.Count; i++)
        {
            beginUpgrade[i].Load(ref data, version, (UpgradeData.Type)i);
        }
    }
}
