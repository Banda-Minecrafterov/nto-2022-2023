using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Character))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    float horizontal;
    float vertical;

    [SerializeField]
    float diagonallSpeed = 85.0f;
    [SerializeField]
    float speed = 80.0f;

    [SerializeField]
    float dashSpeed;
    float dashTime;
    [SerializeField]
    float startDashTime;
    int direction;

    Vector2 lastChangedMovement = Vector2.zero;
    Vector2 animValues = Vector2.zero;

    Character character;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        character = GetComponent<Character>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");

        Roll();
    }

    void FixedUpdate()
    {
        Vector2 move = new Vector2(horizontal, vertical);

        if (move.x == 0.0f || move.y == 0.0f)
        {
            animValues = move;

            character.animator.SetFloat("Speed X", animValues.x);
            character.animator.SetFloat("Speed Y", animValues.y);

            lastChangedMovement = move;

            move *= speed;
        }
        else
        {
            if (lastChangedMovement.x != move.x)
            {
                if (lastChangedMovement.y != move.y)
                {
                    if (UnityEngine.Random.Range(0, 1) == 0)
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

                character.animator.SetFloat("Speed X", animValues.x);
                character.animator.SetFloat("Speed Y", animValues.y);
            }
            else
            {
                if (lastChangedMovement.y != move.y)
                {
                    animValues = new Vector2(0.0f, move.y);

                    character.animator.SetFloat("Speed X", animValues.x);
                    character.animator.SetFloat("Speed Y", animValues.y);
                }
            }
            lastChangedMovement = move;

            move = move.normalized * diagonallSpeed;
        }

        rb.velocity = move;
    }

    void OnDisable()
    {
        rb.velocity = Vector2.zero;
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
}
