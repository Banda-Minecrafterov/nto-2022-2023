using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CharacterAttack : MonoBehaviour
{
    Character character;

    new Collider2D collider;

    [SerializeField]
    string attackName;

    [SerializeField]
    float damagePercentage;
    [SerializeField]
    float damage;


    void Awake()
    {
        collider  = GetComponent<Collider2D>();
        character = GetComponentInParent<Character>();

#if DEBUG
        collider.enabled = false;
#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttackable(collision))
        {
            collision.GetComponent<Character>().TakeDamage(character.GetAttack(damagePercentage, damage));
        }
    }


    public virtual void StartAnimation()
    {
        character.animator.SetBool(attackName, true);
    }

    public virtual void StartAttack()
    {
        collider.enabled = true;
    }

    public virtual void StopAttack()
    {
        collider.enabled = false;
    }

    public virtual void StopAnimation()
    {
        character.animator.SetBool(attackName, false);
    }


    protected abstract bool IsAttackable(Collider2D collision);
}
