using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LoadingSceen");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.LogWarning("Paused Game");
    }

    public void CountinueGame()
    {
        Time.timeScale = 1f;
        Debug.LogWarning("Countinue Game");
    }

    public void ExitGame()
    {
        GameManager.instance.SaveGame();
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainGame");
        GameManager.instance.SaveGame();
        GameManager.instance.state = GameManager.LevelState.Jupiter;
    }
}