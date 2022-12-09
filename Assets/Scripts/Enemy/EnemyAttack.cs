using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    static List<EnemyAttack> everyAttack = new List<EnemyAttack>();


    void Awake()
    {
        everyAttack.Add(this);
    }


    protected override bool IsAttackable(Collider2D collision)
    {
        return collision.CompareTag("Player");
    }


    public static void DisableAttacks()
    {
        foreach (var i in everyAttack)
        {
            i.gameObject.SetActive(false);
        }
    }

    public static void EnableAttacks()
    {
        foreach (var i in everyAttack)
        {
            i.gameObject.SetActive(true);
        }
    }
}
