using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool gameIsPaused = false;

    [SerializeField] GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

                if (gameIsPaused)
                {
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
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPaused = false;
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(0);
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
