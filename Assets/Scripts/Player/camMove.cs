using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -1);
    public GameObject player;
    void Start()
    {

    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
