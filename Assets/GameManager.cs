using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject deathUI;

    public TMP_Text scoreText;
    public TMP_Text deathScoreText;

    public bool gameRunning = false;
    public double score = 10;

    private void Start()
    {
        gameRunning = false;

        menuUI.SetActive(true);
        gameUI.SetActive(false);
        deathUI.SetActive(false);
    }

    public void StartGame()
    {
        score = 10;
        gameRunning = true;

        menuUI.SetActive(false);
        gameUI.SetActive(true);
        deathUI.SetActive(false);

        UpdateScoreUI();
    }

    public void Die()
    {
        gameRunning = false;

        gameUI.SetActive(false);
        deathUI.SetActive(true);

        deathScoreText.text = "Score: " + score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

public void SetScore(double newScore)
{
    score = newScore;
    UpdateScoreUI();
}

private void UpdateScoreUI()
{
    scoreText.text = score.ToString();
}
}