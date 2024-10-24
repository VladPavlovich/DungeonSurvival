using System.Collections;
using UnityEngine;

public class Aug : Gun
{
    public int burstCount = 3;  // Number of bullets per burst
    public float burstDelay = 0.1f;  // Time between each shot in the burst
    private bool isBursting = false;

    private void Start()
    {
        gunName = "AUG Burst Rifle";
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        reloadTime = 2f;
        fireRate = 2f;  // Time between bursts
        isAutomatic = false;  // Burst fire, not fully automatic
    }

    public override void Shoot()
    {
        if (CanShoot() && !isBursting)
        {
            StartCoroutine(BurstFire());
        }
    }

    private IEnumerator BurstFire()
    {
        isBursting = true;

        for (int i = 0; i < burstCount; i++)
        {
            if (currentAmmo > 0)
            {
                // Fire a single bullet
                currentAmmo--;
                Debug.Log("AUG fired! Bullets left: " + currentAmmo);
                SpawnBullet();

                // Delay before the next bullet in the burst
                yield return new WaitForSeconds(burstDelay);
            }
        }

        UpdateNextFireTime();  // Time before the next burst can happen
        isBursting = false;
    }

    private void SpawnBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instantiate the bullet at the fire point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * 50f;  // Adjust bullet speed as needed
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or fire point is missing!");
        }
    }
}
