using System.Collections;
using UnityEngine;

public class Mage : MonoBehaviour
{
    public float health = 150f;
    private bool isDead = false;

    [SerializeField] private GameObject ammoBoxPrefab; 
    [SerializeField] private float dropChance = 0.25f; 
    [SerializeField] private GameObject projectilePrefab; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private float projectileSpeed = 60f; 
    [SerializeField] private float attackInterval = 3f;

    private Transform player; 
    private GameManager gameManager; 

    private void Start()
    {
        Debug.Log(gameObject.name + " - Mage script is running.");

        Rigidbody rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        if (rb != null)
        {
            rb.freezeRotation = true; 
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(ShootProjectile), attackInterval, attackInterval);
    }

    public void TakeDamage(float damage, string gunName)
    {
        if (isDead) return;

        health -= damage;

        Debug.Log(gameObject.name + " took " + damage + " damage! Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        Debug.Log(gameObject.name + " has died!");

        CancelInvoke(nameof(ShootProjectile));

        if (ammoBoxPrefab != null && Random.value <= dropChance)
        {
            Instantiate(ammoBoxPrefab, transform.position, Quaternion.identity);
            Debug.Log("Ammo box dropped.");
        }

        if (gameManager != null)
        {
            gameManager.ShowVictoryPanel();
        }

        Destroy(gameObject, 1.5f); 
    }

    private void ShootProjectile()
    {
        if (isDead || player == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Vector3 direction = (player.position - firePoint.position).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

        Debug.Log("Mage fired a projectile at the player!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            TakeDamage(bullet.damage, bullet.gunName);
            Destroy(bullet.gameObject); // Destroy bullet on impact
        }
    }
}
