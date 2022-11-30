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
            if (!PauseMenu.isPaused && Input.GetButtonDown("Interact"))
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

    protected void StopButtonCheck()
    {
        StopCoroutine(interactCheck);
    }


    protected abstract void Interact();
}
