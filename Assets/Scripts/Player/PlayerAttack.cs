using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : CharacterAttack
{
    protected override bool IsAttackable(Collider2D collision)
    {
        return collision.CompareTag("Enemy");
    }
}
