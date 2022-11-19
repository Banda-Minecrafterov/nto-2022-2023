using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Coroutine interactCheck;


    void Awake()
    {
        interactCheck = StartCoroutine(InteractCheck());
    }


    IEnumerator InteractCheck()
    {
        while (true)
        {
            if (Input.GetAxisRaw("Interact") != 0)
            {
                Interact();
                yield return new WaitForSeconds(0.2f);
            }
            yield return null;
        }
    }


    protected abstract void Interact();
}
