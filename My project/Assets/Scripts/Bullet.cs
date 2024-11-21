using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; 
    public string gunName;     

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with other bullets
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet ignored collision with another bullet: " + other.gameObject.name);
            return;
        }

        Debug.Log("Bullet collided with: " + other.gameObject.name); 

        EnemySkeleton enemy = other.GetComponent<EnemySkeleton>();
        if (enemy != null)
        {
            Debug.Log("Bullet hit the skeleton and will call TakeDamage!");
            enemy.TakeDamage(damage, gunName);
        }

        Destroy(gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet ignored collision with another bullet: " + collision.gameObject.name);
            return;
        }

        Debug.Log("Bullet collision detected with: " + collision.gameObject.name);

        EnemySkeleton enemy = collision.gameObject.GetComponent<EnemySkeleton>();
        if (enemy != null)
        {
            Debug.Log("Bullet hit the skeleton and will call TakeDamage!");
            enemy.TakeDamage(damage, gunName);
        }

        Destroy(gameObject); 
    }
}
