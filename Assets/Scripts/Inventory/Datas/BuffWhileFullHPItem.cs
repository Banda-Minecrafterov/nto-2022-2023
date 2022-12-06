using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Items/Buff While Full HP Item")]
public class BuffWhileFullHPItem : AlwaysActiveItem
{
    public CharacterBuff buff;


    public override bool StartChecking(GameSlot item)
    {
        CharacterHealth health = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterHealth>();
        if (health.currentHealth == health.maxHealth)
        {
            AddBuff(health.gameObject);
        }
        return base.StartChecking(item);
    }

    public override void End(GameSlot item)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().DeleteBuff(buff);
    }


    public override void RestoreHealth(GameSlot item, bool isFull)
    {
        if (isFull)
        {
            AddBuff();
        }
    }
    public override void TakeDamage(GameSlot item, bool isDead)
    {
        End(item);
    }


    void AddBuff()
    {
        AddBuff(GameObject.FindGameObjectWithTag("Player"));
    }

    void AddBuff(GameObject player)
    {
        player.GetComponent<Character>().AddBuff(buff);
    }
}
