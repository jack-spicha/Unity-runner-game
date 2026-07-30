using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject deathUI;

    public bool gameRunning = false;

    private void Start()
    {
        gameRunning = false;

        menuUI.SetActive(true);
        gameUI.SetActive(false);
        deathUI.SetActive(false);
    }

    public void StartGame()
    {
        gameRunning = true;

        menuUI.SetActive(false);
        gameUI.SetActive(true);
        deathUI.SetActive(false);
    }

    public void Die()
    {
        gameRunning = false;

        gameUI.SetActive(false);
        deathUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}