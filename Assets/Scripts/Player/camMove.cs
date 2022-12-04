using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(11.5f, 19.0f, -1.0f);
    public GameObject player;
    void Start()
    {

    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
