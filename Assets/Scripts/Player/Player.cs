using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    Rigidbody2D rb;

    float horizontal;
    float vertical;
    [SerializeField]
    float DiagonallySpeed = 85.0f;
    [SerializeField]
    float speed = 80.0f;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    bool isNotAttacking = true;

    Vector2 lastChangedMovement = Vector2.zero;
    Vector2 animValues = Vector2.zero;


    new void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        base.Awake();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");

        Roll();
    }


    void FixedUpdate()
    {
        if (isNotAttacking)
        {
            Vector2 move = new Vector2(horizontal, vertical);

            if (move.x == 0.0f || move.y == 0.0f)
            {
                animValues = move;

                animator.SetFloat("Speed X", animValues.x);
                animator.SetFloat("Speed Y", animValues.y);

                lastChangedMovement = move;

                move *= speed;
            }
            else
            {
                if (lastChangedMovement.x != move.x)
                {
                    if (lastChangedMovement.y != move.y)
                    {
                        if (Random.Range(0, 1) == 0)
                        {
                            animValues = new Vector2(move.x, 0.0f);
                        }
                        else
                        {
                            animValues = new Vector2(0.0f, move.y);
                        }
                    }
                    else
                    {
                        animValues = new Vector2(move.x, 0.0f);
                    }

                    animator.SetFloat("Speed X", animValues.x);
                    animator.SetFloat("Speed Y", animValues.y);
                }
                else
                {
                    if (lastChangedMovement.y != move.y)
                    {
                        animValues = new Vector2(0.0f, move.y);

                        animator.SetFloat("Speed X", animValues.x);
                        animator.SetFloat("Speed Y", animValues.y);
                    }
                }
                lastChangedMovement = move;

                move = move.normalized * DiagonallySpeed;
            }

            rb.velocity = move;
        }
        else
            rb.velocity = Vector2.zero;
    }


    void OnEnable()
    {
        StartCoroutine(Attack());
    }


    void Roll()
    {
        if (direction == 0)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftShift))
            {               
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 2;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction = 4;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                //roll = false;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    //roll = true;
                    rb.velocity = Vector2.up * dashSpeed;
                }
                else if (direction == 2)
                {
                    //roll = true;
                    rb.velocity = Vector2.down * dashSpeed;
                }
                else if (direction == 3)
                {
                    //roll = true;
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else
                {
                    //roll = true;
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }


    IEnumerator Attack()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isNotAttacking = false;
                yield return StartCoroutine(attack[0].Attack("Player Attack 0"));
                isNotAttacking = true;
            }
            yield return null;
        }
    }
}

