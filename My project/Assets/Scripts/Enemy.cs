using UnityEngine;

public enum EnemySize
{
    Small,
    Medium,
    Large
}

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public EnemySize enemySize;  // Set the size of the enemy (Small, Medium, Large)

    // Apply damage to the enemy
    public void TakeDamage(float baseDamage, string gunName)
    {
        float damageMultiplier = GetDamageMultiplier(gunName);
        float actualDamage = baseDamage * damageMultiplier;

        health -= actualDamage;
        Debug.Log(gameObject.name + " took " + actualDamage + " damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    // Get the damage multiplier based on the gun and enemy size
    private float GetDamageMultiplier(string gunName)
    {
        switch (enemySize)
        {
            case EnemySize.Large:
                if (gunName.Contains("Shotgun"))
                    return 3.0f;  // Shotguns deal triple damage to large enemies
                return 0.5f;  // Other guns deal reduced damage to large enemies

            case EnemySize.Medium:
                if (gunName.Contains("AK") || gunName.Contains("Aug"))
                    return 2.5f;  // AK-47 and AUG deal more damage to medium enemies
                return 0.75f;  // Other guns deal reduced damage to medium enemies

            case EnemySize.Small:
                return 1.0f;  // All guns deal normal damage to small enemies
        }

        return 1.0f;  // Default case
    }

    // Handle enemy death
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        // Add death logic (e.g., play animation, destroy enemy object)
        Destroy(gameObject);  // Destroy the enemy for now
    }
}
