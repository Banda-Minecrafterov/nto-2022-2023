using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TipManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseDisable;

    [SerializeField]
    Transform tooltip;
    [SerializeField]
    GameObject buttonPress;
    [SerializeField]
    ItemSlot item;
    [SerializeField]
    GameObject itemGet;
    [SerializeField]
    GameObject itemTake;

    static TipManager tipManager;


    void Awake()
    {
        tipManager = this;

#if DEBUG
        buttonPress.SetActive(false);

        tooltip.gameObject.SetActive(false);
#endif
    }


    public static void Pause()
    {
        tipManager.pauseDisable.SetActive(PauseMenu.isPaused);
    }


    public static void TooltipEnable(InventoryItemData data)
    {
        tipManager.tooltip.gameObject.SetActive(true);

        int description = tipManager.tooltip.childCount - 1;
        tipManager.tooltip.GetChild(0).GetComponent<TMP_Text>().text           = data.displayName;
        tipManager.tooltip.GetChild(description).GetComponent<TMP_Text>().text = data.description;

        int count = description;
        Transform current = null;

        switch (data)
        {
            case HealItemData obj:
                count = 1;

                current = tipManager.tooltip.GetChild(count);
                current.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = obj.heal.ToString();
                break;

            case BuffWhileFullHPItemData obj:
                count = 2;

                current = tipManager.tooltip.GetChild(count);
                current.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = (obj.buff.attackPercentage * 100.0f - 100.0f).ToString("0.0");
                break;
        }

        current.gameObject.SetActive(true);

        for (int i = 1; i < count; i++)
        {
            tipManager.tooltip.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = count + 1; i < description; i++)
        {
            tipManager.tooltip.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void TooltipFollow(Vector2 pos)
    {
        tipManager.tooltip.position = Input.mousePosition;
    }

    public static void TooltipDisable()
    {
        tipManager.tooltip.gameObject.SetActive(false);
    }


    public static void TipButtonEnable()
    {
        tipManager.buttonPress.SetActive(true);
    }

    public static void TipButtonDisable()
    {
        tipManager.buttonPress.SetActive(false);
    }


    public static IEnumerator ItemGet(UInt32 id, UInt32 count)
    {
        tipManager.itemGet.SetActive(true);
        tipManager.item.gameObject.SetActive(true);

        tipManager.item.Init(id, count);

        yield return new WaitForSeconds(1.0f);

        tipManager.item.gameObject.SetActive(false);
        tipManager.itemGet.SetActive(false);
    }

    public static IEnumerator ItemTake(UInt32 id, UInt32 count)
    {
        tipManager.itemTake.SetActive(true);
        tipManager.item.gameObject.SetActive(true);

        tipManager.item.Init(id, count);

        yield return new WaitForSeconds(1.0f);

        tipManager.item.gameObject.SetActive(false);
        tipManager.itemTake.SetActive(false);
    }
}
