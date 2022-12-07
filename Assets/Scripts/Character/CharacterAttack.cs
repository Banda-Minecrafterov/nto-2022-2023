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
    float chargeTime;
    [SerializeField]
    float damagePercentage;
    [SerializeField]
    float damage;
    [SerializeField]
    float rechargeTime;


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


    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(chargeTime);
        collider.enabled = true;
        character.animator.SetBool(attackName, true);
        yield return new WaitWhile(() => character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        character.animator.SetBool(attackName, false);
        collider.enabled = false;
        yield return new WaitForSeconds(rechargeTime);
    }


    protected abstract bool IsAttackable(Collider2D collision);
}
