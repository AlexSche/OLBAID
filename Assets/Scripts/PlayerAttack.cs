using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayers;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void useBasicAttack() {
        //play attack animation
        //animator.SetTrigger("BasicAttack");
        //detect enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
            foreach (Collider2D enemy in hitEnemies) {
                if (enemy.name == "EnemyImp") {
                    Debug.Log("We hit enemy " + enemy.name);
                    enemy.GetComponent<EnemyInteraction>().takeDamage(attackDamage);
                }
        }
    }

    public void useWhipWhirl() {
        //animator.SetTrigger("WhirlAttack");
        //detect enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);
            foreach (Collider2D enemy in hitEnemies) {
                if (enemy.name == "EnemyImp") {
                    Debug.Log("We hit enemy " + enemy.name);
                    enemy.GetComponent<EnemyInteraction>().takeDamage(attackDamage);
                }
        }
    }
    /*
    void OnDrawGizmosSelected() {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
    */
}
