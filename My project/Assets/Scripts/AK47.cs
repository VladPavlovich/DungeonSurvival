using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Gun
{
    private void Start()
    {
        gunName = "AK-47";
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        reloadTime = 2.5f;
        fireRate = 10f;  // 10 shots per second
        isAutomatic = true;  // Automatic firing
    }

    private void Update()
    {
        // For automatic firing, we continuously check if the fire button is held down
        if (Input.GetMouseButton(0))  // Left mouse button held down
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (CanShoot())
        {
            currentAmmo--;
            Debug.Log("AK-47 fired! Bullets left: " + currentAmmo);
            UpdateNextFireTime();  // Set the next time this gun can fire based on fireRate
            SpawnBullet();  // Call method to fire a bullet
        }
    }
}
