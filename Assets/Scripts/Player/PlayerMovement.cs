using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Character))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    new Collider2D collider;

    float horizontal;
    float vertical;

    [SerializeField]
    float diagonallSpeed = 85.0f;
    [SerializeField]
    float speed = 80.0f;
    [SerializeField]
    float dashSpeed = 80.0f;
    [SerializeField]
    float dashTime = 80.0f;

    Vector2 lastChangedMovement = Vector2.zero;

    Player character;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        character = GetComponent<Player>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (horizontal == 0.0f && vertical == 0.0f)
            {
                var state = character.animator.GetCurrentAnimatorStateInfo(0);
                if (state.IsName("Up_idle"))
                {
                    StartCoroutine(Dash(new Vector2(0.0f, 1.0f)));
                }
                else if (state.IsName("Down_idle"))
                {
                    StartCoroutine(Dash(new Vector2(0.0f, -1.0f)));
                }
                else if (state.IsName("Left_idle"))
                {
                    StartCoroutine(Dash(new Vector2(-1.0f, 0.0f)));
                }
                else if (state.IsName("Right_idle"))
                {
                    StartCoroutine(Dash(new Vector2(1.0f, 0.0f)));
                }
            }
            else
            {
                StartCoroutine(Dash(new Vector2(horizontal, vertical)));
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 move = new Vector2(horizontal, vertical);

        if (move.x == 0.0f || move.y == 0.0f)
        {
            character.animator.SetFloat("Speed X", move.x);
            character.animator.SetFloat("Speed Y", move.y);

            lastChangedMovement = move;

            move *= speed;
        }
        else
        {
            if (lastChangedMovement.x != move.x)
            {
                if (lastChangedMovement.y == move.y || Random.Range(0, 1) == 0)
                {
                    character.animator.SetFloat("Speed X", move.x);
                    character.animator.SetFloat("Speed Y", 0.0f);
                }
                else
                {
                    character.animator.SetFloat("Speed X", 0.0f);
                    character.animator.SetFloat("Speed Y", move.y);
                }
            }
            else
            {
                if (lastChangedMovement.y != move.y)
                {
                    character.animator.SetFloat("Speed X", 0.0f);
                    character.animator.SetFloat("Speed Y", move.y);
                }
            }
            lastChangedMovement = move;

            move = move.normalized * diagonallSpeed;
        }

        rb.velocity = move;
    }

    void OnDisable()
    {
        horizontal = 0.0f;
        vertical = 0.0f;

        rb.velocity = Vector2.zero;
    }


    IEnumerator Dash(Vector2 direction)
    {
        if (character.stamina.DecreaseStamina(20.0f))
        {
            if (direction.x == 0.0f || direction.y == 0.0f)
            {
                direction *= dashSpeed;
            }
            else
            {
                direction = direction.normalized * dashSpeed * diagonallSpeed / speed;
            }

            character.enabled = false;
            EnemyAttack.DisableAttacks();

            float time = 0.0f;
            while (time < dashTime)
            {
                rb.velocity = direction;

                yield return new WaitForSeconds(Time.fixedDeltaTime);

                time += Time.fixedDeltaTime;
            }
            rb.velocity = Vector2.zero;

            EnemyAttack.EnableAttacks();
            character.enabled = true;
        }
    }
}
