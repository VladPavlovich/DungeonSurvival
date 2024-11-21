using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public GameObject gunPrefab; 
    private bool isPlayerNearby = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is near " + gunPrefab.name + ". Press Q to pick up.");
            isPlayerNearby = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the area.");
            isPlayerNearby = false; 
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Player picked up " + gunPrefab.name);

            PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddGunToInventory(gunPrefab);
                Destroy(gameObject); 
            }
            else
            {
                Debug.LogError("PlayerInventory script not found on Player!");
            }
        }
    }
}
