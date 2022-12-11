using Pathfinding;
using UnityEngine;
using UnityEngine.TextCore.Text;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyMovement : AIPath
{
    Enemy enemy;

    Vector2 lastChangedMovement = Vector2.zero;


    protected override void Awake()
    {
        enemy = GetComponent<Enemy>();
        base.Awake();
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 move = desiredVelocity;

        if (move.x == 0.0f || move.y == 0.0f)
        {
            enemy.animator.SetFloat("Speed X", move.x);
            enemy.animator.SetFloat("Speed Y", move.y);

            lastChangedMovement = move;

            if (move.x == 0.0f && move.y == 0.0f)
            {
                AudioManager.playerSnowWalk.mute = true;
            }
            else
            {
                AudioManager.playerSnowWalk.mute = false;
            }
        }
        else
        {
            if (lastChangedMovement.x != move.x)
            {
                if (lastChangedMovement.y == move.y || Random.Range(0, 1) == 0)
                {
                    enemy.animator.SetFloat("Speed X", move.x);
                    enemy.animator.SetFloat("Speed Y", 0.0f);
                }
                else
                {
                    enemy.animator.SetFloat("Speed X", 0.0f);
                    enemy.animator.SetFloat("Speed Y", move.y);
                }
            }
            else
            {
                if (lastChangedMovement.y != move.y)
                {
                    enemy.animator.SetFloat("Speed X", 0.0f);
                    enemy.animator.SetFloat("Speed Y", move.y);
                }
            }
            lastChangedMovement = move;
        }
    }


    public override void OnTargetReached()
    {
        enemy.StartAttackAnimation();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (reachedDestination)
        {
            OnTargetReached();
        }
    }
}