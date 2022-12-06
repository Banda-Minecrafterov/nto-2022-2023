using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Collider2D))]
public abstract class CharacterAttack : MonoBehaviour
{
    Character character;

    new Collider2D collider;
    
    public float chargeTime { get; private set; }
    public float rechargeTime { get; private set; }

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
            collision.GetComponent<Character>().TakeDamage(damage * character.GetAttackPercantageBuff());
        }
    }


    public IEnumerator Attack(string attackName)
    {
        collider.enabled = true;
        character.animator.SetBool(attackName, true);
        yield return new WaitWhile(() => character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        character.animator.SetBool(attackName, false);
        collider.enabled = false;
    }


    protected abstract bool IsAttackable(Collider2D collision);
}
