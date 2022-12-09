using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterHealth))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    CharacterHealth health;
    [SerializeField]
    protected CharacterAttack[] attack;
    protected CharacterAttack currentAttack = null;

    [SerializeField]
    List<CharacterBuff> tempBuffs;
    [SerializeField]
    CharacterBuff stats;

    public float pureAttackPercantage
    {
        get
        {
            float pureAttackPercantage = stats.attackPercentage;

            foreach (var i in tempBuffs)
            {
                pureAttackPercantage /= i.attackPercentage;
            }

            return pureAttackPercantage;
        }
    }

    public float pureAttackPlus
    {
        get
        {
            float pureAttackPlus = stats.attackPlus;

            foreach (var i in tempBuffs)
            {
                pureAttackPlus -= i.attackPlus;
            }

            return pureAttackPlus;
        }
    }

    public float pureMaxHealthPercantage
    {
        get
        {
            float pureMaxHealthPercantage = stats.maxHealthPercantage;

            foreach (var i in tempBuffs)
            {
                pureMaxHealthPercantage /= i.maxHealthPercantage;
            }

            return pureMaxHealthPercantage;
        }
    }

    public float pureMaxHealthPlus
    {
        get
        {
            float pureMaxHealthPlus = stats.maxHealthPlus;

            foreach (var i in tempBuffs)
            {
                pureMaxHealthPlus -= i.maxHealthPlus;
            }

            return pureMaxHealthPlus;
        }
    }

    public float maxHealth
    {
        get
        {
            return GetMaxHealth(1.0f, stats.maxHealthPercantage, 0.0f, stats.maxHealthPlus);
        }
    }

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


    public void AddTempBuff(CharacterBuff buff)
    {
        tempBuffs.Add(buff);
        AddBuff(buff);
    }

    public bool RemoveTempBuff(CharacterBuff buff)
    {
        if (tempBuffs.Remove(buff))
        {
            RemoveBuff(buff);
            return true;
        }
        return false;
    }


    public void AddPermBuff(CharacterBuff buff)
    {
        AddBuff(buff);
    }


    void AddBuff(CharacterBuff buff)
    {
        stats.attackPercentage *= buff.attackPercentage;
        stats.attackPlus       += buff.attackPlus;

        stats.maxHealthPercantage *= buff.maxHealthPercantage;
        stats.maxHealthPlus       += buff.maxHealthPlus;
    }

    void RemoveBuff(CharacterBuff buff)
    {
        stats.attackPercentage /= buff.attackPercentage;
        stats.attackPlus       -= buff.attackPlus;

        stats.maxHealthPercantage /= buff.maxHealthPercantage;
        stats.maxHealthPlus       -= buff.maxHealthPlus;
    }


    public float GetAttack(float attackPercantage, float attackPlus)
    {
        return GetAttack(attackPercantage, stats.attackPercentage, attackPlus, stats.attackPlus);
    }


    public static float GetAttack(float attackPercantage, float origAttackPercantage, float attackPlus, float origAttackPlus)
    {
        return attackPercantage * origAttackPercantage * (attackPlus + origAttackPlus);
    }

    public static float GetMaxHealth(float maxHealthPercantage, float origMaxHealthPercantage, float maxHealthPlus, float origMaxHealthPlus)
    {
        return maxHealthPercantage * origMaxHealthPercantage * (maxHealthPlus + origMaxHealthPlus);
    }


    protected virtual void StartAttackAnimation(int attackId)
    {
        currentAttack = attack[attackId];
        currentAttack.StartAnimation();
    }

    public virtual void StartAttack()
    {
        currentAttack.StartAttack();
    }

    public virtual void StopAttack()
    {
        currentAttack.StopAttack();
    }

    public virtual void StopAttackAnimation()
    {
        currentAttack.StopAnimation();
    }
}
