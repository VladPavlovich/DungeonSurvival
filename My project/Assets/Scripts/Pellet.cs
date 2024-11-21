using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float damage = 5f; 
    public string gunName = "Shotgun"; 

    [SerializeField] private GameObject impactEffect; 

    private void OnCollisionEnter(Collision collision)
    {
        EnemySkeleton skeleton = collision.gameObject.GetComponent<EnemySkeleton>();
        Golem golem = collision.gameObject.GetComponent<Golem>();
        LittleDemon demon = collision.gameObject.GetComponent<LittleDemon>();

        if (skeleton != null)
        {
            skeleton.TakeDamage(damage, gunName);
        }
        else if (golem != null)
        {
            golem.TakeDamage(damage, gunName);
        }
        else if (demon != null)
        {
            demon.TakeDamage(damage, gunName);
        }

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
