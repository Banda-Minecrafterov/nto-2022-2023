using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    protected override void Die()
    {
        Debug.Log("Player dead");
    }
}
