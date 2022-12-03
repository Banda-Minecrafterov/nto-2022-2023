using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterHealth))]
public class Character : MonoBehaviour
{
    CharacterHealth health;
    [SerializeField]
    protected CharacterAttack[] attack;


    void Awake()
    {
        health = GetComponent<CharacterHealth>();
    }


    public void TakeDamage(int damage)
    {
        if (health.TakeDamage(damage))
        {
            health.enabled = false;
            enabled = false;
        }
    }
}
