using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{
    public GameObject demonPrefab;         
    public Transform[] spawnPoints;        
    public int demonsToSpawn = 10;          
    public GameObject doorToLevel6;        

    private bool levelStarted = false;
    private List<GameObject> spawnedDemons = new List<GameObject>();

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
        Debug.Log("Starting Level 5: Shotgun Room with Little Demons");

        for (int i = 0; i < demonsToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject demon = Instantiate(demonPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedDemons.Add(demon);
        }
    }

    private void Update()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        spawnedDemons.RemoveAll(demon => demon == null);

        if (spawnedDemons.Count == 0 && levelStarted)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level 5 completed Destroying door to Level 6.");

        if (doorToLevel6 != null)
        {
            Destroy(doorToLevel6);
        }
        else
        {
            Debug.LogWarning("Door to Level 6 not assigned.");
        }

        levelStarted = false;  
    }
}
