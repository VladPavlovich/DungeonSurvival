using System.Collections;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    public GameObject bossPrefab;          
    public Transform spawnPoint;           
    public GameObject doorToNextLevel;     

    private GameObject boss;              
    private bool levelStarted = false;     

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelStarted)
        {
            levelStarted = true;
            StartLevel();
        }
    }

    private void StartLevel()
    {
        Debug.Log("Starting Level 8: Boss Room");

        if (bossPrefab != null && spawnPoint != null)
        {
            boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Mage boss spawned.");
        }
        else
        {
            Debug.LogError("Boss prefab or spawn point not assigned!");
        }
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        if (boss == null && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 8 completed Destroying door to the next level.");

        if (doorToNextLevel != null)
        {
            Destroy(doorToNextLevel);
        }
        else
        {
            Debug.LogWarning("Door to the next level not assigned.");
        }

        levelStarted = false;
    }
}
