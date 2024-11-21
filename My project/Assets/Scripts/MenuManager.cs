using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject deathScreenPanel;

    private bool isPaused = false;

    private void Start()
    {
        ShowStartMenu(); 
    }

    public void ShowStartMenu()
    {
        startMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        deathScreenPanel.SetActive(false);
        Time.timeScale = 0f; 
    }

    public void HideAllMenus()
    {
        startMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        deathScreenPanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void ShowPauseMenu()
    {
        if (isPaused)
        {
            HideAllMenus();
            isPaused = false;
        }
        else
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f; 
            isPaused = true;
        }
    }

    public void ShowDeathScreen()
    {
        deathScreenPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
}
