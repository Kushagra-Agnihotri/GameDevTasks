using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

using TMPro;

public class EndScreen: MonoBehaviour
{
    public GameObject gameOverUI;
    public TMP_Text endScoreText;
    public TMP_Text highScoreText;

    public void DisplayScores(int endScore, int highScore)
    {
        endScoreText.text = "Your Score: " + endScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
