using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject enemyPrefab;         
    public Transform[] spawnPoints;       
    public int enemiesToSpawn = 5;          
    public GameObject doorToLevel3;        

    private bool levelStarted = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

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
        Debug.Log("Starting Level 2: Use Pistol to Get AK-47");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(enemy);
        }
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        // If all enemies are destroyed, complete the level
        if (spawnedEnemies.Count == 0 && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 2 completed Destroying door to Level 3.");

        if (doorToLevel3 != null)
        {
            Destroy(doorToLevel3);
        }
        else
        {
            Debug.LogWarning("Door to Level 3 not assigned.");
        }

        levelStarted = false;
    }
}
