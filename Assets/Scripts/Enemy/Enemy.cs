using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : Character
{
    EnemyMovement path;

    Coroutine startAttacking;


    new void Awake()
    {
        path = GetComponent<EnemyMovement>();

        base.Awake();
    }


    public void StartAttacking()
    {
        startAttacking = StartCoroutine(Attack());
    }

    public bool StopAttacking()
    {
        try
        {
            StopCoroutine(startAttacking);
        }
        catch { return false; }
        return true;
    }


    IEnumerator Attack()
    {
        while (true)
        {
            path.enabled = false;
            yield return new WaitForSeconds(attack[0].chargeTime);
            StartCoroutine(attack[0].Attack("Enemy Attack 0"));
            yield return new WaitForSeconds(attack[0].rechargeTime);
            path.enabled = true;
            yield return null;
        }
    }
}
