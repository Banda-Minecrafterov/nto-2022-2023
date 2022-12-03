using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform EnemyAttackPoint;

    public LayerMask LayerPlayer;
    public float attackTime = 1f;

    private float attackRange = 1f;
    public int damage = 20;

    Player playerRoll;

    void Start()
    {
        playerRoll = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(WaitForAttack(attackTime));
        
    }

    void Update()
    {

    }
    void Attack()
    {
        Collider2D[] enemyHit = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, LayerPlayer);

        //animator.SetTrigger("Attack");
        foreach (Collider2D player in enemyHit)
        {            
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            
        }      
    }
    private IEnumerator WaitForAttack(float attackTime)
    {
        while (true)
        {
            if (playerRoll.roll == false)
            {
                Attack();
                yield return new WaitForSeconds(attackTime);
            }         
        }      
    }
    private void OnDrawGizmosSelected()
    {
        if (EnemyAttackPoint == null)
            return;

        Gizmos.DrawSphere(EnemyAttackPoint.position, attackRange);
    }
}
