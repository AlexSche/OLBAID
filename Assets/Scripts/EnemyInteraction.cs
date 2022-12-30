using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;
    public HealthbarScript healthbar;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
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
        }
    }

    private void die () {
        Debug.Log("Enemy died!");
    }
}
