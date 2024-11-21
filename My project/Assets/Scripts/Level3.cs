using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public GameObject enemyPrefab;         
    public Transform[] swarmSpawnPoints;    
    public int enemiesToSpawn = 10;       
    public float spawnInterval = 1f;      
    public GameObject doorToLevel4;        

    private bool levelStarted = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int totalSpawnedEnemies = 0;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelStarted)
        {
            levelStarted = true;
            StartCoroutine(StartLevel());
        }
    }

    private IEnumerator StartLevel()
    {
        Debug.Log("Starting Level 3: Use AK to Kill Swarms");

        while (totalSpawnedEnemies < enemiesToSpawn)
        {
            foreach (Transform spawnPoint in swarmSpawnPoints)
            {
                if (totalSpawnedEnemies >= enemiesToSpawn)
                {
                    break; // Stop spawning if we've reached the limit
                }

                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnedEnemies.Add(enemy);
                totalSpawnedEnemies++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("All enemies have been spawned.");
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (spawnedEnemies.Count == 0 && levelStarted && totalSpawnedEnemies == enemiesToSpawn)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 3 completed Destroying door to Level 4.");

        if (doorToLevel4 != null)
        {
            Destroy(doorToLevel4);
        }
        else
        {
            Debug.LogWarning("Door to Level 4 not assigned.");
        }

        // Mark levelStarted as false to prevent further checks
        levelStarted = false;
    }
}
