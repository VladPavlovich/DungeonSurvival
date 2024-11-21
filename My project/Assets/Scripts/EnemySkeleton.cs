using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : MonoBehaviour
{
    public float health = 60f;
    private bool isDead = false;
    private Rigidbody rb;



    [SerializeField] private GameObject ammoBoxPrefab; 
    [SerializeField] private float dropChance = 0.25f; 


    private void Start()
    {
        Debug.Log(gameObject.name + " - EnemySkeleton script is running.");
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true;  
        }
    }

    public void TakeDamage(float damage, string gunName)
    {
        if (isDead) return;

        float damageMultiplier = gunName.Contains("Pistol") ? 1.5f : 1f;  // Extra damage from pistols
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
            Vector3 spawnPosition = new Vector3(transform.position.x, -1.158f, transform.position.z);

            Instantiate(ammoBoxPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Ammo box dropped at position: " + spawnPosition);
        }

        Destroy(gameObject, 1f); 
    }


    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            TakeDamage(bullet.damage, bullet.gunName);
            Destroy(bullet.gameObject);  // Destroy bullet on impact
        }
    }
}
