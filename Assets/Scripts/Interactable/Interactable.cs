using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Coroutine interactCheck;


    void Awake()
    {
        StartButtonCheck();
    }


    IEnumerator InteractCheck()
    {
        while (true)
        {
            if (Input.GetButtonDown("Interact") && !PauseMenu.isPaused)
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
    }


    protected abstract void Interact();
}
