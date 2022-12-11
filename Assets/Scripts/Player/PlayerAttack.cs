using UnityEngine;

public class PlayerAttack : CharacterAttack
{
    [SerializeField]
    AudioSource vzmax;


    protected override bool IsAttackable(Collider2D collision)
    {
        return collision.CompareTag("Enemy");
    }


    public override void StartAttack()
    {
        vzmax.Play();
        base.StartAttack();
    }

    public override void StopAttack()
    {
        base.StopAttack();
    }
}
