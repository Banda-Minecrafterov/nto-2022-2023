using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : Character
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    float DiagonallySpeed = 0.7f;
    public float speed = 20.0f;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    public bool roll = false;
    private bool isFacingRight = true;

    PlayerHealth game;



    void Start()
    {
        game = GetComponent<PlayerHealth>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (!isFacingRight && horizontal > 0)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0)
        {
            Flip();
        }

        Roll();
    }


    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= DiagonallySpeed;
            vertical *= DiagonallySpeed;
        }
        if (roll == false)
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }       
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
                roll = false;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    roll = true;
                    rb.velocity = Vector2.up * dashSpeed;
                }
                else if (direction == 2)
                {
                    roll = true;
                    rb.velocity = Vector2.down * dashSpeed;
                }
                else if (direction == 3)
                {
                    roll = true;
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else
                {
                    roll = true;
                    rb.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}

