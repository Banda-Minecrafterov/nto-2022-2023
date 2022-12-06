using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    CharacterHealth health;
    [SerializeField]
    protected CharacterAttack[] attack;
    [SerializeField]
    List<CharacterBuff> buffs;

    public Animator animator { get; private set; }


    protected void Awake()
    {
        health   = GetComponent<CharacterHealth>();
        animator = GetComponent<Animator>();
    }


    public void TakeDamage(float damage)
    {
        if (health.TakeDamage(damage))
        {
            health.enabled = false;
            enabled = false;
        }
    }


    public void AddBuff(CharacterBuff buff)
    {
        buffs.Add(buff);
    }

    public bool DeleteBuff(CharacterBuff buff)
    {
        return buffs.Remove(buff);
    }

    public float GetAttackPercantageBuff()
    {
        float percantage = 1.0f;
        foreach (var i in buffs)
        {
            percantage *= i.attackPercentage;
        }
        return percantage;
    }
}
