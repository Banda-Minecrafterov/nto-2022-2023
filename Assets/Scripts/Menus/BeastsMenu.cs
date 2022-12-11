using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeastsMenu : BaseMenu
{
    [SerializeField]
    Transform descriptions;
    [SerializeField]
    Transform entries;

    public static BeastsMenu menu {get; private set; }


    public void Awake()
    {
        menu = this;

        gameObject.SetActive(false);
    }


    public static void AddEnemy(int id)
    {
        menu.entries.GetChild(id).GetComponent<Button>().interactable = true;
    }

    public void ShowBeast(Transform button)
    {
        descriptions.GetChild(button.GetSiblingIndex()).gameObject.SetActive(true);
    }


    void OnDisable()
    {
        foreach (Transform i in descriptions)
        {
            i.gameObject.SetActive(false);
        }
    }
}
