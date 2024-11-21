using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public GameObject startPanel; 
    public GameObject deathPanel; 
    public GameObject victoryPanel; 
    public GameObject dummyCamera; 

    private void Start()
    {
        if (dummyCamera != null)
        {
            dummyCamera.SetActive(true);
        }

        ShowStartPanel();
    }

    private void Update()
    {
        if (startPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (deathPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }

        if (victoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game button or space bar pressed!");
        HideAllPanels();

        if (dummyCamera != null)
        {
            dummyCamera.SetActive(false);
        }

        Debug.Log("Game started.");
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game by reloading the scene.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowStartPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (dummyCamera != null)
        {
            dummyCamera.SetActive(true);
        }

        HideAllPanels();
        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }
    }

    public void ShowDeathPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (dummyCamera != null)
        {
            dummyCamera.SetActive(true);
        }

        HideAllPanels();
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    public void ShowVictoryPanel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (dummyCamera != null)
        {
            dummyCamera.SetActive(true);
        }

        HideAllPanels();
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }

    private void HideAllPanels()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (deathPanel != null) deathPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }
}
