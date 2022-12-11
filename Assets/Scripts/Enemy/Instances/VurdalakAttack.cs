using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VurdalakAttack : EnemyAttack
{
    bool isPlayer = false;
    int count = 0;
    [SerializeField]
    int maxCount;
    [SerializeField]
    CharacterBuff buff;
    [SerializeField]
    AudioSource rik;


    new void Awake()
    {
        character = GetComponentInParent<Character>();

        base.Awake();
    }


    protected override bool IsAttackable(Collider2D collision)
    {
        bool attackable = base.IsAttackable(collision);
        isPlayer |= attackable;
        return attackable;
    }


    public override void StopAttack()
    {
        base.StopAttack();

        if (isPlayer)
        {
            isPlayer = false;
        }
        else
        {
            count++;

            if (count == maxCount)
            {
                rik.Play();
                character.AddTempBuff(buff);
                count = 0;
            }
        }
    }
}
