using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;

    public LayerMask LayerEnemy;

    private float attackRange = 1f;
    private int Damage = 20;

    PlayerHealth game;
    Player playerRoll;

    void Start()
    {
        game = GetComponent<PlayerHealth>();
        playerRoll = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerRoll.roll == false && game.gameOver == false)
        {
            Attack();
        }
    }
    void Attack()
    {
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerEnemy);

        //animator.SetTrigger("Attack");

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHP>().TakeDamage(Damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
