using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour
{
    private static bool gameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;
    public static NewPauseMenu pauseInstance;

    void Awake()
    {
        if (pauseInstance != null)
        {
            return;
        }
        pauseInstance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gameIsPaused)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Resume();
                }
                else
                {
                    Paused();
                }
        }
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Restart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Resume();
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
