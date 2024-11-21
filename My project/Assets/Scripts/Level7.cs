using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject demonPrefab;
    public GameObject golemPrefab;

    public Transform[] skeletonSpawnPoints; 
    public Transform[] demonSpawnPoints;   
    public Transform[] golemSpawnPoints;   

    public GameObject doorToLevel8;        

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
        Debug.Log("Starting Level 7: Mixed Threats Requiring Weapon Switching");

        SpawnEnemies(skeletonPrefab, skeletonSpawnPoints);

        SpawnEnemies(demonPrefab, demonSpawnPoints);

        SpawnEnemies(golemPrefab, golemSpawnPoints);
    }

    private void SpawnEnemies(GameObject enemyPrefab, Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (enemyPrefab != null)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (spawnedEnemies.Count == 0 && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 7 completed Destroying door to Level 8.");

        if (doorToLevel8 != null)
        {
            Destroy(doorToLevel8);
        }
        else
        {
            Debug.LogWarning("Door to Level 8 not assigned.");
        }

        levelStarted = false;
    }
}
