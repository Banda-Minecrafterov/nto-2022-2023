using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Save : Interactable
{
    protected override void Interact()
    {
        Debug.Log(Time.timeScale);
        PauseMenu.saveMenu.SetActive(true);
    }
}
