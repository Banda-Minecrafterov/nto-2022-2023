using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float chaseRadius;
    public float attackRadius;
    public float moveSpeed;
    private bool isFacingRight = false;

    public Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        CheckDistance();

    }



    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                if (target.transform.position.x < gameObject.transform.position.x && isFacingRight)
                    Flip();
                if (target.transform.position.x > gameObject.transform.position.x && !isFacingRight)
                    Flip();
            }
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;

    }
}