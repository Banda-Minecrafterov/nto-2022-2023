using UnityEngine;


[CreateAssetMenu(menuName = "Items/Buff While Full HP Item")]
public class BuffWhileFullHPItemData : AlwaysActiveItemData
{
    public CharacterBuff buff;


    public override bool StartChecking(GameSlot item)
    {
        if (Player.player.GetComponent<CharacterHealth>().currentHealth == Player.player.maxHealth)
        {
            AddBuff();
        }
        return base.StartChecking(item);
    }

    public override void End(GameSlot item)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().RemoveTempBuff(buff);
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
        Player.player.AddTempBuff(buff);
    }
}
