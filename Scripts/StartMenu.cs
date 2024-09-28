using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Play"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); 
    }
}
