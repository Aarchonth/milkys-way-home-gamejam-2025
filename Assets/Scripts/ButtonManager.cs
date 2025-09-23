using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public Button startButton;
    public Button countinueButton;
    public Button exitButton;

    void Start()
    {
        // Listener zuweisen
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame);

    }
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
            Debug.Log("Exit Game")´;
        }
    

    
    void Update()
    {
        
    }
}
