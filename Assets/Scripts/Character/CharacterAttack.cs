using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CharacterAttack : MonoBehaviour
{
    public float chargeTime;
    [SerializeField]
    float attackTime;
    public float rechargeTime;

    new Collider2D collider;

    [SerializeField]
    int damage;


    void Awake()
    {
        collider = GetComponent<Collider2D>();

#if DEBUG
        collider.enabled = false;
#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttackable(collision))
        {
            collision.GetComponent<Character>().TakeDamage(damage);
        }
    }


    public IEnumerator Attack()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(attackTime);
        collider.enabled = false;
    }


    protected abstract bool IsAttackable(Collider2D collision);
}
