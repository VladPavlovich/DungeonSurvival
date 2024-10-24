using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private void Start()
    {
        gunName = "Glock";
        maxAmmo = 15;
        currentAmmo = maxAmmo;
        reloadTime = 1.5f;
        fireRate = 2f;  // 2 shots per second
        isAutomatic = false;
    }

    public override void Shoot()
    {
        if (CanShoot())
        {
            currentAmmo--;
            Debug.Log("Glock shot! Bullets left: " + currentAmmo);
            UpdateNextFireTime();  // Ensure the next fire time is set
            SpawnBullet();  // Call method to fire a bullet (capsule)
        }
    }
}