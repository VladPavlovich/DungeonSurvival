using UnityEngine;

public class Pistol : Gun
{
    private void Start()
    {
        gunName = "Glock";
        maxAmmo = 15;
        currentAmmo = maxAmmo;
        reloadTime = 1.5f;
        fireRate = 2f;  
        isAutomatic = false;
    }

    public override void Shoot()
    {
        if (CanShoot())
        {
            currentAmmo--;
            Debug.Log("Glock shot! Bullets left: " + currentAmmo);
            UpdateNextFireTime();
            SpawnBullet();
            ApplyRecoil(); 
            PlayGunSound();
        }
    }
}
