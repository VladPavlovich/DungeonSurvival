using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public GameObject golemPrefab;          
    public Transform bossSpawnPoint;       
    public GameObject doorToLevel5;        

    private GameObject spawnedBoss;
    private bool levelStarted = false;
    private bool shotgunUnlocked = false;  

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
        Debug.Log("Starting Level 4: Boss Battle with Golem");

\        if (golemPrefab != null && bossSpawnPoint != null)
        {
            spawnedBoss = Instantiate(golemPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
            Debug.Log("Golem boss spawned for Level 4.");
        }
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        if (spawnedBoss == null && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 4 completed! Unlocking shotgun and destroying door to Level 5.");

        UnlockShotgun();

        if (doorToLevel5 != null)
        {
            Destroy(doorToLevel5);
        }
        else
        {
            Debug.LogWarning("Door to Level 5 not assigned.");
        }

        levelStarted = false; 
    }

    private void UnlockShotgun()
    {
        shotgunUnlocked = true;
        Debug.Log("Shotgun unlocked Player can now access the shotgun.");
    }
}
