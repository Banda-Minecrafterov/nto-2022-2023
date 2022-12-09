using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastsMenu : BaseMenu
{
    [SerializeField]
    Transform descriptions;


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
