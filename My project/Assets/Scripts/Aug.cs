using UnityEngine;
using System.Collections;

public class Aug : Gun
{
    public int burstCount = 3;  
    public float burstDelay = 0.1f;
    private bool isBursting = false;

    private void Start()
    {
        gunName = "Aug";
        maxAmmo = 30;
        currentAmmo = maxAmmo;
        reloadTime = 2f;
        fireRate = 2f;  
        isAutomatic = false; 

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
                currentAmmo--;
                Debug.Log("AUG fired! Bullets left: " + currentAmmo);
                SpawnBullet();

                yield return new WaitForSeconds(burstDelay);
            }
        }

        UpdateNextFireTime();
        isBursting = false;
        ApplyRecoil();
        PlayGunSound();
    }
}
