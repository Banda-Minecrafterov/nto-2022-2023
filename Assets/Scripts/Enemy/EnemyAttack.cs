using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    protected override bool IsAttackable(Collider2D collision)
    {
        return Player.isAtackable && collision.CompareTag("Player");
    }
}
