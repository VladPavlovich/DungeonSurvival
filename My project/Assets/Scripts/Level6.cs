using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour
{
    public GameObject skeletonPrefab;      
    public GameObject demonPrefab;         
    public GameObject golemPrefab;          

    public Transform[] hallwaySpawnPoints;  
    public GameObject doorToLevel7;        

    private int currentWave = 0;
    private bool levelStarted = false;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !levelStarted)
        {
            levelStarted = true;
            StartNextWave();
        }
    }

    private void Update()
    {
        CheckWaveCompletion();
    }

    private void StartNextWave()
    {
        currentWave++;
        Debug.Log("Starting Wave " + currentWave + " in Level 6");

        GameObject enemyPrefab = null;
        if (currentWave == 1) enemyPrefab = skeletonPrefab;
        else if (currentWave == 2) enemyPrefab = demonPrefab;
        else if (currentWave == 3) enemyPrefab = golemPrefab;

        foreach (Transform spawnPoint in hallwaySpawnPoints)
        {
            if (enemyPrefab != null)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    private void CheckWaveCompletion()
    {
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        if (spawnedEnemies.Count == 0 && levelStarted)
        {
            if (currentWave < 3)
            {
                StartNextWave();
            }
            else
            {
                CompleteLevel();
            }
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 6 completed Destroying door to Level 7.");

        if (doorToLevel7 != null)
        {
            Destroy(doorToLevel7);
        }
        else
        {
            Debug.LogWarning("Door to Level 7 not assigned.");
        }

        levelStarted = false;
    }
}
