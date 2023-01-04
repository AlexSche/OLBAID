using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    public Animator animator;
    private EnemyMovement enemyMovement;
    public HealthbarScript healthbar;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);
        if (currentHealth <= 0) {
            die();
            enemyMovement.enabled = false;
        }
    }

    private void die() {
        //animator.SetBool("isDead", true);
    }

    public void removeEnemy() {
        //remove Enemy after 3 seconds delay
        Destroy(this.gameObject, 3);
    }
}
