using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    protected Coroutine interactCheck;
    protected bool isTouching { get; private set; } = false; 


    void Awake()
    {
        StartButtonCheck();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTouching = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isTouching = false;
    }


    IEnumerator InteractCheck()
    {
        while (true)
        {
            if (isTouching && !PauseMenu.isPaused && Input.GetButtonDown("Interact"))
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
