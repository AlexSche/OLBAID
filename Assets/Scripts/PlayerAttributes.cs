using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthbarScript healthbar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void takeDamage(int damage) {
        currentHealth -= damage;
        healthbar.setHealth(currentHealth);
        if (currentHealth <= 0) {
            Debug.Log("DIE!");
        }
    }
}
