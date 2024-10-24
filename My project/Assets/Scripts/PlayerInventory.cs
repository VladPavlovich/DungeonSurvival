using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject pistolPrefab;
    public GameObject ak47Prefab;
    public GameObject shotgunPrefab;
    public GameObject augPrefab;  

    private GameObject currentGun;

    public Transform gunHolder;

    private int currentGunIndex = 0;
    private List<GameObject> gunPrefabs = new List<GameObject>();

    private void Start()
    {
        // Add the prefabs to the list
        gunPrefabs.Add(pistolPrefab);
        gunPrefabs.Add(ak47Prefab);
        gunPrefabs.Add(shotgunPrefab);
        gunPrefabs.Add(augPrefab);  // Added AUG to the list

        // Start the game with the pistol prefab instantiated at the gun holder
        EquipGun(0);  // Equip pistol at start
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button to shoot
        {
            if (currentGun != null)
            {
                Gun gunComponent = currentGun.GetComponent<Gun>();
                if (gunComponent != null)
                {
                    gunComponent.Shoot();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && gunPrefabs.Count > 1)
        {
            SwitchGuns();
        }
    }

    // Equip the gun by instantiating the prefab at the gun holder position
    private void EquipGun(int index)
    {
        if (index < gunPrefabs.Count)
        {
            if (currentGun != null)
            {
                Destroy(currentGun);
            }

            currentGun = Instantiate(gunPrefabs[index], gunHolder.position, Quaternion.identity);  // Use identity rotation

            currentGun.transform.SetParent(gunHolder);

            currentGun.transform.localPosition = Vector3.zero;  

            if (currentGun.name.Contains("Shotgun"))
            {
                // Adjust rotation for shotgun
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 180, 0));
            }
            else if (currentGun.name.Contains("Aug"))
            {
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 90, 0));
            }
            else
            {
                // Default rotation for other guns
                currentGun.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));  // Modify if needed for other guns
            }
        }
    }

    private void SwitchGuns()
    {
        currentGunIndex = (currentGunIndex + 1) % gunPrefabs.Count;
        EquipGun(currentGunIndex);
    }
}
