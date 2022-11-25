using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CurseTriangle : MonoBehaviour
{
    [HideInInspector]
    public CurseMaster master;

    [SerializeField]
    float minR;


    public void Teleport(Vector3 dir)
    {
        if (dir.magnitude < minR)
        {
            dir.Normalize();
            dir *= minR;
        }

        transform.localPosition = dir;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            master.Purification();
        }
    }
}
