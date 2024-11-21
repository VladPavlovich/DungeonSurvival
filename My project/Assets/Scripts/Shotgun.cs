using UnityEngine;

public class Shotgun : Gun
{
    public int pelletCount = 5; 
    public float spreadAngle = 0.05f; 
    public float pelletSpeed = 20f; 
    public float pelletLifetime = 5f;

    private void Start()
    {
        gunName = "Shotgun";
        maxAmmo = 8; 
        currentAmmo = maxAmmo;
        reloadTime = 3f; 
        fireRate = 1f; 
        isAutomatic = false; 
    }

    public override void Shoot()
    {
        if (CanShoot())
        {
            currentAmmo--;
            Debug.Log("Shotgun fired! Bullets left: " + currentAmmo);

            FirePellets();

            UpdateNextFireTime(); 
            ApplyRecoil();
            PlayGunSound();
        }
    }

    private void FirePellets()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is missing! Please assign a valid prefab in the Inspector.");
            return;
        }

        if (firePoint == null)
        {
            Debug.LogError("Fire point is missing! Please assign a valid fire point in the Inspector.");
            return;
        }

        for (int i = 0; i < pelletCount; i++)
        {
            // Calculate random spread angles (tighter spread with reduced range)
            float angleX = Random.Range(-spreadAngle, spreadAngle); // Vertical spread (pitch)
            float angleY = Random.Range(-spreadAngle, spreadAngle); // Horizontal spread (yaw)

            Vector3 spreadDirection = Quaternion.Euler(angleX, angleY, 0) * firePoint.forward;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(spreadDirection));

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = spreadDirection * pelletSpeed; 
            }

            Destroy(bullet, pelletLifetime);

            Debug.Log($"Spawned pellet {i + 1}/{pelletCount} with spread at {Time.time}");
        }
    }
}
