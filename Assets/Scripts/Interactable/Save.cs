using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : Interactable
{
    protected override void Interact()
    {
        Debug.Log(Time.timeScale);
        PauseMenu.saveMenu.SetActive(true);
    }
}
