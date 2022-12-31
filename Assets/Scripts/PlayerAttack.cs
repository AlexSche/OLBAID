using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackpoint;
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
        animator.SetTrigger("BasicAttack");
        //detect enemies hit
        Collider[] hitEnemies = Physics.OverlapSphere(attackpoint.position,attackRange,enemyLayers);
        foreach (Collider enemy in hitEnemies) {
            //deal damage to enemies hit
            Debug.Log("We hit enemy " + enemy.name);
            enemy.GetComponent<EnemyInteraction>().takeDamage(attackDamage);
        }
    }

    public void useWhipWhirl() {
        animator.SetTrigger("WhirlAttack");
                Collider[] hitEnemies = Physics.OverlapSphere(attackpoint.position,attackRange,enemyLayers);
        foreach (Collider enemy in hitEnemies) {
            //deal damage to enemies hit
            Debug.Log("We hit enemy " + enemy.name);
            enemy.GetComponent<EnemyInteraction>().takeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected() {
        if (attackpoint == null)
            return;
        Gizmos.DrawSphere(attackpoint.position, attackRange);
    }
}
