using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public abstract class CharacterAttack : MonoBehaviour
{
    public static List<AudioSource> audioSources { get; private set; } = new List<AudioSource>();

    protected Character character;

    new Collider2D collider;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    string attackName;

    [SerializeField]
    float damagePercentage;
    [SerializeField]
    float damage;


    protected void Awake()
    {
        character = GetComponentInParent<Character>();
        collider  = GetComponent<Collider2D>();

        audioSources.Add(source);

#if DEBUG
        collider.enabled = false;
#endif
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttackable(collision))
        {
            source.Play();
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
