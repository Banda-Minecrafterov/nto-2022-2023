using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
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
    float dashSpeed = 1.0f;

    Vector2 lastChangedMovement = Vector2.zero;

    Player character;

    Coroutine dash;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

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
                    dash = StartCoroutine(StartDash(new Vector2(0.0f, 1.0f)));
                }
                else if (state.IsName("Down_idle"))
                {
                    dash = StartCoroutine(StartDash(new Vector2(0.0f, -1.0f)));
                }
                else if (state.IsName("Left_idle"))
                {
                    dash = StartCoroutine(StartDash(new Vector2(-1.0f, 0.0f)));
                }
                else if (state.IsName("Right_idle"))
                {
                    dash = StartCoroutine(StartDash(new Vector2(1.0f, 0.0f)));
                }
            }
            else
            {
                dash = StartCoroutine(StartDash(new Vector2(horizontal, vertical)));
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

            AudioManager.playerSnowWalk.mute = false;
        }

        rb.velocity = move;
    }

    void OnDisable()
    {
        horizontal = 0.0f;
        vertical   = 0.0f;

        rb.velocity = Vector2.zero;

        AudioManager.playerSnowWalk.mute = true;
    }


    IEnumerator StartDash(Vector2 direction)
    {
        if (character.stamina.DecreaseStamina(20.0f))
        {
            character.animator.SetFloat("Speed X", direction.x);
            character.animator.SetFloat("Speed Y", direction.y);

            yield return null;

            if (direction.x == 0.0f || direction.y == 0.0f)
            {
                direction *= dashSpeed;
            }
            else
            {
                direction = direction.normalized * (dashSpeed * diagonallSpeed / speed);
            }

            AudioManager.playerDash.enabled = true;
            character.enabled = false;
            character.animator.SetBool("Dash", true);

            while (true)
            {
                rb.velocity = direction;
                yield return null;
            }
        }
    }


    public void StopDash()
    {
        StopCoroutine(dash);

        AudioManager.playerDash.enabled = false;
        character.enabled = true;
        character.animator.SetBool("Dash", false);
    }
}
