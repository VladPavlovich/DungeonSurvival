using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject enemyPrefab;          
    public Transform[] spawnPoints;         
    public int enemiesToSpawn = 3;         
    public GameObject doorToLevel2;         

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
        Debug.Log("Starting Level 1: Pistol Room");

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

        if (spawnedEnemies.Count == 0 && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 1 completed! Destroying door to Level 2.");

        if (doorToLevel2 != null)
        {
            Destroy(doorToLevel2);
        }
        else
        {
            Debug.LogWarning("Door to Level 2 not assigned.");
        }

        levelStarted = false;
    }
}
