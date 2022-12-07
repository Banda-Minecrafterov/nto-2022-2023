using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Heal Item")]
public class HealItemData : UsableItemData
{
    public int heal;


    protected override bool IsUsed()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>().RestoreHealth(heal);
    }
}
