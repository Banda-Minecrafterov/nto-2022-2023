using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyChase : MonoBehaviour
{
    static int inCombat = 0;

    public static bool isNotInCombat
    {
        get
        {
            return inCombat == 0;
        }
    }


    EnemyMovement movement;

    new CircleCollider2D collider;

    SpriteRenderer sprite;

    [SerializeField]
    float sawRadius;
    [SerializeField]
    float chaseRadius;


    void Awake()
    {
        movement = GetComponentInParent<EnemyMovement>();
        sprite   = GetComponentInParent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();

#if DEBUG
        movement.enabled = false;
        collider.radius = sawRadius;
#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCombat++;

            collider.radius = chaseRadius;
            movement.enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (movement.desiredVelocity.x > 0)
            {
                sprite.flipX = true;
            }
            else if (movement.desiredVelocity.x < 0)
            {
                sprite.flipX = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inCombat--;

            collider.radius = sawRadius;
            movement.enabled = false;
        }
    }
}
