using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene(einfügen)");
    }

    public void CountinueGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Countinue Game");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }



    void Update()
    {

    }
}
