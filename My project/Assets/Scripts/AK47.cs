using UnityEngine;

public class AK47 : Gun
{
    private bool isActive = false; 

    private void Start()
    {
        gunName = "Ak 47";
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        reloadTime = 2.5f;
        fireRate = 10f;  
        isAutomatic = true;  
    }

    private void Update()
    {
        // Check if this gun is active and isAutomatic
        if (isActive && isAutomatic && Input.GetMouseButton(0))
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
            UpdateNextFireTime();
            SpawnBullet();
            ApplyRecoil();  
            PlayGunSound();
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
