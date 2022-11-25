using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class CurseSource : MonoBehaviour
{
    [HideInInspector]
    public CurseMaster master;

    [HideInInspector]
    public new CircleCollider2D collider;

    [SerializeField]
    float T;


    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            master.Teleport(collision.transform.position);
            StartCoroutine(RechargeTeleportation());
        }
    }


    IEnumerator RechargeTeleportation()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(T);
        collider.enabled = true;
    }
}
