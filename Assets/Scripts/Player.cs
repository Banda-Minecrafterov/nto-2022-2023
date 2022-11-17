using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Transform cam;
    Vector3   forward;
    Vector3   right;

    [SerializeField]
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed;
    float horizontal = 0, vertical = 0;

    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpCooldown;
    [SerializeField]
    float airMultiplier;
    bool  jump = false;

    [Header("Ground")]
    [SerializeField]
    float drag;
    int onGround = 0;


    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();

        forward = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);
        right   = cam.right;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            forward = new Vector3(cam.forward.x, transform.forward.y, cam.forward.z);
            right   = cam.right;
        }


        Vector3 vel = rb.velocity;
        Vector3 care = new Vector3(vel.x, 0, vel.z);

        if (care.magnitude > moveSpeed)
        {
            care = care.normalized * moveSpeed;
            rb.velocity = new Vector3(care.x, vel.y, care.z);
        }


        horizontal += Input.GetAxisRaw("Horizontal");
        vertical   += Input.GetAxisRaw("Vertical");

        if (onGround != 0 && !IsInvoking(nameof(JumpReset)) && Input.GetAxisRaw("Jump") == 1)
        {
            jump = true;

            Invoke(nameof(JumpReset), jumpCooldown);
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 || vertical != 0)
        {
            if (onGround == 0)
                rb.AddForce((forward * vertical + right * horizontal).normalized * moveSpeed * airMultiplier, ForceMode.Force);
            else
                rb.AddForce((forward * vertical + right * horizontal).normalized * moveSpeed,                 ForceMode.Force);

            horizontal = 0;
            vertical   = 0;
        }
        if (jump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            jump = false;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        onGround += 1;

        if (onGround == 1)
        {
            rb.drag = drag;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onGround -= 1;

        if (onGround == 0)
        {
            rb.drag = 0;
        }
    }


    void JumpReset()
    {
        Vector3 vel = rb.velocity;
        rb.velocity = new Vector3(vel.x, 0, vel.z);
    }
}
