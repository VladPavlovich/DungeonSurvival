using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public GameObject pistolPrefab;
    public GameObject ak47Prefab;
    public GameObject shotgunPrefab;
    public GameObject augPrefab;

    public TextMeshProUGUI ammoText; 

    private GameObject currentGun;
    private Gun currentGunComponent;

    public Transform gunHolder;

    private int currentGunIndex = 0;
    private List<GameObject> unlockedGuns = new List<GameObject>();

    private Dictionary<string, int> ammoInventory = new Dictionary<string, int>
    {
        { "Glock", 15 },
        { "Ak 47", 30 },
        { "Shotgun", 5 },
        { "Aug", 20 }
    };

    private Dictionary<string, int> magazineInventory = new Dictionary<string, int>
    {
        { "Glock", 3 },
        { "Ak 47", 3 },
        { "Shotgun", 3 },
        { "Aug", 3 }
    };

    private void Start()
    {
        unlockedGuns.Add(pistolPrefab);
        EquipGun(0);
        UpdateAmmoUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentGunComponent != null)
        {
            if (currentGunComponent.currentAmmo > 0)
            {
                currentGunComponent.Shoot();
                ammoInventory[currentGunComponent.gunName] = currentGunComponent.currentAmmo;
                UpdateAmmoUI();
            }
            else
            {
                Debug.Log("Out of ammo! Reload needed.");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGunComponent != null)
        {
            ReloadGun();
        }

        if (Input.GetKeyDown(KeyCode.Z) && unlockedGuns.Count > 1)
        {
            SwitchGuns();
        }
    }

    public void ReloadGun()
    {
        if (currentGunComponent == null) return;

        string gunName = currentGunComponent.gunName;

        if (magazineInventory[gunName] > 0)
        {
            int ammoNeeded = currentGunComponent.maxAmmo - currentGunComponent.currentAmmo;

            if (ammoNeeded > 0)
            {
                currentGunComponent.currentAmmo += ammoNeeded;
                magazineInventory[gunName]--;
                currentGunComponent.currentAmmo = Mathf.Min(currentGunComponent.currentAmmo, currentGunComponent.maxAmmo);
                ammoInventory[gunName] = currentGunComponent.currentAmmo; 
                Debug.Log($"Reloaded {gunName}. Current ammo: {currentGunComponent.currentAmmo}. Magazines left: {magazineInventory[gunName]}");
            }
            else
            {
                Debug.Log($"{gunName} already has a full magazine!");
            }
        }
        else
        {
            Debug.Log($"No magazines left for {gunName}!");
        }

        UpdateAmmoUI();
    }

    private void EquipGun(int index)
    {
        if (index < unlockedGuns.Count)
        {
            if (currentGun != null)
            {
                Destroy(currentGun);
                currentGunComponent = null;
            }

            currentGun = Instantiate(unlockedGuns[index], gunHolder.position, Quaternion.identity);
            currentGun.transform.SetParent(gunHolder);
            currentGun.transform.localPosition = Vector3.zero;

            if (currentGun.name.Contains("Shotgun"))
            {
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 180, 0));
            }
            else if (currentGun.name.Contains("Aug"))
            {
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 90, 0));
            }
            else if (currentGun.name.Contains("Ak"))
            {
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 90));
            }
            else
            {
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
            }

            currentGunComponent = currentGun.GetComponent<Gun>();

            if (currentGunComponent != null)
            {
                string gunName = currentGunComponent.gunName;
                currentGunComponent.currentAmmo = ammoInventory.ContainsKey(gunName) ? ammoInventory[gunName] : currentGunComponent.maxAmmo;

                if (currentGunComponent is AK47 ak47Gun)
                {
                    ak47Gun.SetActive(true); // Allow automatic fire only for the active AK47
                    Debug.Log("AK-47 is now active and ready for automatic fire.");
                }
            }

            UpdateAmmoUI();
        }
    }

    private void SwitchGuns()
    {
        currentGunIndex = (currentGunIndex + 1) % unlockedGuns.Count;
        EquipGun(currentGunIndex);
    }

    private void UpdateAmmoUI()
    {
        if (currentGunComponent != null && ammoText != null)
        {
            string gunName = currentGunComponent.gunName;
            int ammoCount = currentGunComponent.currentAmmo; 
            int magCount = magazineInventory.ContainsKey(gunName) ? magazineInventory[gunName] : 0;
            ammoText.text = $"Ammo: {ammoCount}/{currentGunComponent.maxAmmo} | Magazines: {magCount}";
        }
    }

    public void AddGunToInventory(GameObject gunPrefab)
    {
        if (!unlockedGuns.Contains(gunPrefab))
        {
            unlockedGuns.Add(gunPrefab);
            Debug.Log($"{gunPrefab.name} added to inventory!");
        }
        else
        {
            Debug.LogWarning($"{gunPrefab.name} is already in the inventory!");
        }
    }

    public void AddMagazine(string gunName, int amount)
    {
        if (magazineInventory.ContainsKey(gunName))
        {
            magazineInventory[gunName] += amount;
            Debug.Log($"{amount} magazines added to {gunName}. Total: {magazineInventory[gunName]}");
        }
        else
        {
            Debug.LogWarning($"{gunName} is not in the magazine inventory!");
        }

        UpdateAmmoUI();
    }

    public int GetAmmo(string gunName)
    {
        return ammoInventory.ContainsKey(gunName) ? ammoInventory[gunName] : 0;
    }
}
