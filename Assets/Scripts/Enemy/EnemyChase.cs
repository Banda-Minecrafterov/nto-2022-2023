using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyChase : MonoBehaviour
{
    AIPath path;

    new CircleCollider2D collider;

    SpriteRenderer sprite;

    [SerializeField]
    float sawRadius;
    [SerializeField]
    float chaseRadius;


    void Awake()
    {
        path     = GetComponentInParent<AIPath>();
        sprite   = GetComponentInParent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();

#if DEBUG
        path.enabled = false;
        collider.radius = sawRadius;
#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider.radius = chaseRadius;
            path.enabled = true;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (path.desiredVelocity.x >= float.Epsilon)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collider.radius = sawRadius;
            path.enabled = false;
        }
    }
}
