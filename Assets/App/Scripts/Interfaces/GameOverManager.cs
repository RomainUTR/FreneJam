using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        PlayerState.isAlive = true;
        Time.timeScale = 1f;
        PlayerState.lives = 10;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
