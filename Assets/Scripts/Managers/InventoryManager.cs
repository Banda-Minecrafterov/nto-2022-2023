using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Transform gameSlots;

    [SerializeField]
    InventoryItemData[] datas;

    public static InventoryManager manager { get; private set; }

    public static InventoryItemData[] items
    {
        get
        {
            return manager.datas;
        }
    }


    void Awake()
    {
        manager = this;
    }


    public static Transform GetGameSlot(int count)
    {
        return manager.gameSlots.GetChild(count);
    }


    public static void TakeDamage(bool isDead)
    {
        foreach (Transform i in manager.gameSlots)
        {
            i.GetComponent<GameSlot>().TakeDamage(isDead);
        }
    }

    public static void RestoreHealth(bool isFull)
    {
        foreach (Transform i in manager.gameSlots)
        {
            i.GetComponent<GameSlot>().RestoreHealth(isFull);
        }
    }
}
