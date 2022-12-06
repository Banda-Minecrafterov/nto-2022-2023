using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : AIPath
{
    Enemy enemy;

    protected override void Awake()
    {
        enemy = GetComponent<Enemy>();

        base.Awake();
    }


    public override void OnTargetReached()
    {
        enemy.StartAttacking();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (waitingForPathCalculation)
        {
            enemy.StopAttacking();
        }
    }
}