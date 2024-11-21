using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public TextMeshProUGUI healthText; 

    private GameManager gameManager; 
    private bool isDead = false; 

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false; 
        UpdateHealthUI();

        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + ", current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            UpdateHealthUI();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false; 
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
        else
        {
            Debug.LogWarning("Health Text UI element not assigned.");
        }
    }

    private void Die()
    {
        if (isDead) return; 
        isDead = true;

        Debug.Log("Player has died!");
        if (gameManager != null)
        {
            gameManager.ShowDeathPanel();
        }

        // Deactivate the player object until respawn
       // gameObject.SetActive(false);
    }


    public bool IsDead()
    {
        return currentHealth <= 0;
    }

}
