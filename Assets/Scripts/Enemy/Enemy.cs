using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(AIPath))]
public class Enemy : Character
{
    AIPath path;


    void Awake()
    {
        path = GetComponent<AIPath>();

        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        while (true)
        {
            while (path.reachedEndOfPath)
            {
                path.enabled = false;
                yield return new WaitForSeconds(attack[0].chargeTime);
                StartCoroutine(attack[0].Attack());
                yield return new WaitForSeconds(attack[0].rechargeTime);
                path.enabled = true;
            }
            yield return null;
        }
    }
}
