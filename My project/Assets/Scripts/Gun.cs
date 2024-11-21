using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public string gunName;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public float fireRate;   
    public bool isAutomatic;

    public GameObject bulletPrefab;   
    public Transform firePoint;       

    private float nextFireTime = 0f;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning($"Gun {gunName} has no AudioSource attached! Gun sounds won't play.");
        }
    }

    public abstract void Shoot();

    public virtual void Reload(int ammoToLoad)
    {
        if (currentAmmo < maxAmmo && ammoToLoad > 0)
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            int ammoToReload = Mathf.Min(ammoToLoad, ammoNeeded);

            currentAmmo += ammoToReload;
            Debug.Log($"{gunName} reloaded. Current ammo: {currentAmmo}/{maxAmmo}");
        }
        else
        {
            Debug.Log($"{gunName} is already fully loaded or no spare ammo.");
        }
    }

    protected bool CanShoot()
    {
        return Time.time >= nextFireTime && currentAmmo > 0;
    }

    protected void UpdateNextFireTime()
    {
        nextFireTime = Time.time + 1f / fireRate;
    }

    protected void SpawnBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody rb = bulletObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * 50f;
            }

            Destroy(bulletObject, 5f);
        }
        else
        {
            Debug.LogError("Bullet prefab or fire point is missing!");
        }
    }

    protected void ApplyRecoil()
    {
        GunRecoil recoil = GetComponent<GunRecoil>();
        if (recoil != null)
        {
            recoil.ApplyRecoil(); 
        }
        else
        {
            Debug.LogWarning($"Gun {gunName} has no GunRecoil component attached.");
        }
    }

    protected void PlayGunSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
