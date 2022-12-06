using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    Coroutine interactCheck;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartButtonCheck();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StopButtonCheck();
    }


    IEnumerator InteractCheck()
    {
        while (true)
        {
            if (EnemyChase.isNotInCombat && !PauseMenu.isPaused && Input.GetButtonDown("Interact"))
            {
                Interact();
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }


    protected void StartButtonCheck()
    {
        interactCheck = StartCoroutine(InteractCheck());
        TipManager.TipButtonEnable();
    }

    protected bool StopButtonCheck()
    {
        try
        {
            StopCoroutine(interactCheck);
        }
        catch { return false; }

        TipManager.TipButtonDisable();
        return true;
    }


    protected abstract void Interact();
}
