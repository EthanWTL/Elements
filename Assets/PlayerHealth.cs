using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public GameOverScreen gameOverScreen;

    Animator animator;
    public float maxHealth = 5f;
    public float currentHealth;

    GameObject healthbarObject;
    HealthBar healthbar;

    private void Start()
    {
        healthbarObject = GameObject.FindGameObjectWithTag("HealthBar");
        healthbar = healthbarObject.GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthbar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            animator.SetTrigger("death");
        }
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        gameOverScreen.SetUpGameOverScreen();
    }


}
