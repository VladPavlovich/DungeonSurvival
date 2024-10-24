using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public int pelletCount = 4;  // Number of pellets fired in a single shot
    public float spreadAngle = 2f;  // Reduce the spread angle for a tighter spread

    private void Start()
    {
        gunName = "Shotgun";
        maxAmmo = 8;  // Typical for a shotgun
        currentAmmo = maxAmmo;
        reloadTime = 3f;  // Longer reload time for the shotgun
        fireRate = 1f;  // 1 shot per second
        isAutomatic = false;  // Shotgun is typically not automatic
    }

    private void Update()
    {
        // Shotgun firing only when the left mouse button is clicked once (not automatic)
        if (Input.GetMouseButtonDown(0))  // Left mouse button clicked
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (CanShoot())
        {
            currentAmmo--;
            Debug.Log("Shotgun fired! Bullets left: " + currentAmmo);
            UpdateNextFireTime();  // Set the next fire time based on fireRate

            // Fire multiple pellets in a spread pattern
            for (int i = 0; i < pelletCount; i++)
            {
                SpawnPellet();  // Call a custom method to spawn each pellet
            }
        }
    }

    private void SpawnPellet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Instantiate the pellet (bullet) without any spread initially
            GameObject pellet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the pellet so that it flies straight initially
            Rigidbody rb = pellet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * 50f;  // Adjust pellet speed if needed

                // Start a coroutine to apply spread after a delay
                StartCoroutine(ApplyDelayedSpread(pellet, rb));
            }
        }
        else
        {
            Debug.LogError("Bullet prefab or fire point is missing!");
        }
    }

    private IEnumerator ApplyDelayedSpread(GameObject pellet, Rigidbody rb)
    {
        // Wait for 1 second (adjust this time to delay the spread effect)
        yield return new WaitForSeconds(1f);

        // Generate a small random angle for spread
        float angleX = Random.Range(-spreadAngle, spreadAngle);  // Vertical (pitch) deviation
        float angleY = Random.Range(-spreadAngle, spreadAngle);  // Horizontal (yaw) deviation

        // Apply the new rotation to the pellet after the delay
        Quaternion spreadRotation = Quaternion.Euler(pellet.transform.rotation.eulerAngles.x + angleX,
                                                     pellet.transform.rotation.eulerAngles.y + angleY,
                                                     pellet.transform.rotation.eulerAngles.z);

        // Recalculate the velocity in the new direction
        rb.velocity = spreadRotation * Vector3.forward * 50f;  // Maintain the same speed but change direction
    }






}
