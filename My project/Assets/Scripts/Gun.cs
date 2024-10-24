using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public float fireRate;  // Fire rate in shots per second
    public bool isAutomatic;

    public GameObject bulletPrefab;  // Prefab for the bullet
    public Transform firePoint;      // The point where bullets are fired from

    private float nextFireTime = 0f;  // Track when the gun can fire next

    public abstract void Shoot();

    protected bool CanShoot()
    {
        // Check if we can shoot (if the current time is past the nextFireTime and we have ammo)
        return Time.time >= nextFireTime && currentAmmo > 0;
    }

    protected void UpdateNextFireTime()
    {
        // Update the next fire time based on the fire rate
        nextFireTime = Time.time + 1f / fireRate;
    }

    protected void SpawnBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instantiate the bullet at the fire point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * 50f;  
            }

            Destroy(bullet, 5f); 
        }
        else
        {
            Debug.LogError("Bullet prefab or fire point is missing!");
        }
    }


}
