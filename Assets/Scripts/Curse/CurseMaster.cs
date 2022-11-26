using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class CurseMaster : MonoBehaviour
{
    [SerializeField]
    CurseSource source;
    [SerializeField]
    CurseTriangle triangle;
    [SerializeField]
    GameObject purification;

    Coroutine curseEffectCoroutine;

    new CircleCollider2D collider;

    [SerializeField]
    float N;
    [SerializeField]
    protected int S;
    [SerializeField]
    float minR;
    [HideInInspector]
    float R;
    [SerializeField]
    float respawnTime;

    protected int stacks { private set; get; }
    Func<int> stackIncreasing;

    bool playerWasInside = false;


    protected bool isIncreasing
    {
        get
        {
            return stackIncreasing == StackBelowS;
        }
    }

    void Start()
    {
        source.master   = this;
        triangle.master = this;

        collider = GetComponent<CircleCollider2D>();

        R = collider.radius;

#if DEBUG
        purification.SetActive(true);
        ChangeState(false);
        source.gameObject.SetActive(true);
        collider.enabled = true;
#endif
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stackIncreasing = StackBelowS;

            playerWasInside = true;

            if (ChangeState(true))
            {
                stacks = -1;
                curseEffectCoroutine = StartCoroutine(CurseEffect());

                triangle.Teleport((transform.position - collision.transform.position).normalized, R);
                purification.transform.localPosition = UnityEngine.Random.insideUnitCircle * R;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayerExitedCurse());
        }
    }


    public void Teleport(Vector3 playerPos)
    {
        var position       = transform.position;
        transform.position = position + (position - playerPos).normalized * UnityEngine.Random.Range(minR, R);
    }


    int StackBelowS()
    {
        stacks += 1;
        if (stacks <= S)
        {
            ApplyEffect();
        }
        else
        {
            Purification();
        }
        return 0;
    }

    int StackBelow2S()
    {
        stacks -= 1;
        if (stacks >= 0)
        {
            ApplyEffect();
        }
        else
        {
            if (ChangeState(false))
            {
                source.gameObject.SetActive(false);
                collider.enabled = false;
            }

            StopCoroutine(curseEffectCoroutine);
            StartCoroutine(Respawn());
        }

        return 0;
    }


    public void Purification()
    {
        if (ChangeState(false))
        {
            source.gameObject.SetActive(false);
            collider.enabled = false;
        }

        stackIncreasing = StackBelow2S;
    }

    bool ChangeState(bool isInCurse)
    {
        if (purification.activeSelf == isInCurse)
            return false;

        triangle.gameObject.SetActive(isInCurse);
               purification.SetActive(isInCurse);

        source.collider.enabled = isInCurse;

        return true;
    }


    protected abstract void ApplyEffect();


    IEnumerator CurseEffect()
    {
        while (true)
        {
            stackIncreasing();
            yield return new WaitForSeconds(N);
        }
    }

    IEnumerator PlayerExitedCurse()
    {
        playerWasInside = false;
        yield return new WaitForSeconds(N / 2.0f);
        if (!playerWasInside)
            stackIncreasing = StackBelow2S;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        collider.enabled = true;
        source.gameObject.SetActive(true);
    }
}
