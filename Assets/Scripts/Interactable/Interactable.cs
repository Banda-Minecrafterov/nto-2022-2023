using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    protected Animator animator { get; private set; }

    Coroutine interactCheck;

    public static bool checkable { get => EnemyChase.isNotInCombat && !PauseMenu.isPaused; }


    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }


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


    protected virtual IEnumerator InteractCheck()
    {
        while (true)
        {
            if (checkable)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    animator.SetBool("Interact", true);
                    Interact();
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }

    public void StopInteracting()
    {
        animator.SetBool("Interact", false);
    }


    protected void StartButtonCheck()
    {
        interactCheck = StartCoroutine(InteractCheck());
        ShowTip(true);
    }

    protected bool StopButtonCheck()
    {
        try
        {
            StopCoroutine(interactCheck);
        }
        catch { return false; }

        ShowTip(false);
        return true;
    }


    protected virtual void Interact() { }
    protected virtual void ShowTip(bool isShow) { TipManager.TipButton(isShow); }
}
