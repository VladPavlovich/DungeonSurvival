using UnityEngine;

public class LittleDemon : MonoBehaviour
{
    public float health = 20f;          
    private bool isDead = false;

    [Header("Ammo Drop Settings")]
    public GameObject ammoBoxPrefab;  
    public float dropChance = 0.25f;   

    public void TakeDamage(float damage, string gunName)
    {
        if (isDead) return;

        float damageMultiplier = gunName.Contains("Shotgun") ? 1.5f : 1.0f;
        float actualDamage = damage * damageMultiplier;

        health -= actualDamage;
        Debug.Log("Little Demon took " + actualDamage + " damage! Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Little Demon has been defeated!");

        TryDropAmmo();

        Destroy(gameObject, 0.5f); 
    }

    private void TryDropAmmo()
    {
        if (ammoBoxPrefab != null && Random.value <= dropChance)
        {
            Instantiate(ammoBoxPrefab, transform.position, Quaternion.identity);
            Debug.Log("Little Demon dropped an ammo box!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        
        if (bullet != null)
        {
            TakeDamage(bullet.damage, bullet.gunName);
            Destroy(bullet.gameObject);  
        }
    }
}
