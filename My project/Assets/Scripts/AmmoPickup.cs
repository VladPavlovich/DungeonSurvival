using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public string gunName; 
    public int magazineAmount = 1; 
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player"))
        {

            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddMagazine(gunName, magazineAmount);
                Debug.Log($"{magazineAmount} magazines added to {gunName}.");
                Destroy(gameObject); // Destroy the ammo pickup
            }
            else
            {
                Debug.LogWarning("PlayerInventory component not found ");
            }
        }
    }
}
