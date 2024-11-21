using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1Trigger; 
    public GameObject level1ExitTrigger;  
    private bool level1Cleared = false;

    private void Update()
    {
        if (!level1Cleared && AllEnemiesDefeated())
        {
            level1Cleared = true;
            ActivateLevel1Exit();
        }
    }

    private bool AllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private void ActivateLevel1Exit()
    {
        Debug.Log("Level 1 Cleared Proceed to the next level.");
        level1ExitTrigger.SetActive(true); 
    }
}

