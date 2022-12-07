using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    protected override bool IsAttackable(Collider2D collision)
    {
        return collision.CompareTag("Player");
    }
}
