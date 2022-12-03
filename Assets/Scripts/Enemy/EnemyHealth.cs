using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
