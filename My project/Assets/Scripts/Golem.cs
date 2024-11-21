using UnityEngine;

public class Golem : MonoBehaviour
{
    public float health = 200f;          
    private bool isDead = false;
    private Rigidbody rb;

    [Header("Ammo Drop Settings")]
    [SerializeField] private GameObject ammoBoxPrefab;  
    [SerializeField] private float dropChance = 0.25f;  

    private void Start()
    {
        Debug.Log(gameObject.name + " - Golem script is running.");

        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;  
        }
    }

    public void TakeDamage(float damage, string gunName)
    {
        if (isDead) return;

        float damageMultiplier = gunName.Contains("Aug") ? 2.0f : 1.0f;
        float actualDamage = damage * damageMultiplier;

        health -= actualDamage;

        Debug.Log(gameObject.name + " took " + actualDamage + " damage! Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        isDead = true;

        if (ammoBoxPrefab != null && Random.value <= dropChance)
        {
            Instantiate(ammoBoxPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ammo box dropped.");
        }

        Destroy(gameObject, 1.5f); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            TakeDamage(bullet.damage, bullet.gunName);
            Destroy(bullet.gameObject);  // Destroy bullet on impact
            return; 
        }

        Pellet pellet = collision.gameObject.GetComponent<Pellet>();
        if (pellet != null)
        {
            TakeDamage(pellet.damage, pellet.gunName);
            Destroy(pellet.gameObject);  
        }
    }

}
