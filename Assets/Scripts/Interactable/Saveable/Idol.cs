using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Idol : SaveableInteractable
{
    public static List<AudioSource> audioSources { get; private set; } = new List<AudioSource>();

    [SerializeField]
    private AudioSource interact;


    bool isSaveableLocal = true;

    public bool isSaveable
    {
        get => isSaveableLocal;
        set
        {
            ShowTip(false);
            isSaveableLocal = value;
            ShowTip(true);
        }
    }


    new void Awake()
    {
        base.Awake();

        audioSources.Add(interact);

        enabled = false;
    }


    public void Activate()
    {
        ShowTip(true);
    }


    protected override IEnumerator InteractCheck()
    {
        while (true)
        {
            if (checkable && enabled)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    interact.Play();
                    PauseMenu.UpgradeMenu();
                    yield return new WaitForSeconds(0.1f);
                }
                if (isSaveable && Input.GetButtonDown("Interact2"))
                {
                    interact.Play();
                    PauseMenu.OpenSaveMenu(this);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }

    protected override void ShowTip(bool isShow)
    {
        if (enabled)
        {
            if (isSaveable)
                TipManager.TipTwoButton(isShow);
            else
                TipManager.TipButton(isShow);
        }
    }


    public override void Load(BinaryReader data, int version)
    {
        if (data.ReadBoolean())
        {
            animator.SetBool("Force_interact", true);
            isSaveableLocal = data.ReadBoolean();
        }
    }

    public override void Save(BinaryWriter data)
    {
        bool enabled = this.enabled;
        data.Write(enabled);

        if (enabled)
            data.Write(isSaveable);
    }
}
