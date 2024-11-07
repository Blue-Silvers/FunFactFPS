using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Level selection for button")]
    [SerializeField] private string FirstLevelToLoad;
    [SerializeField] private string SecondLevelToLoad;
    [SerializeField] private string ThirdLevelToLoad;
    [SerializeField] private string CreditsLoad;

    [Header("Windows")]
    [SerializeField] private GameObject settingsWindow, levelsWindow;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject ngButton, settingsButton, lvlButton;

    [Header("Background")]
    [SerializeField] private GameObject[] background;

    [Header("Input Manager (don't touch)")]
    [SerializeField] private InputActionReference escapeM;
    private void Start()
    {
        Time.timeScale = 1;
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);

        int rdBackground = Random.Range(0, background.Length);
        background[rdBackground].SetActive(true);
        for (int i = 0; i < background.Length; i++)
        {
            if (background[i] != background[rdBackground])
            {
                background[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            CloseSettingsButton();
        }
    }
    public void Level1()
    {
        SceneManager.LoadScene(FirstLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }
    public void Level2()
    {
        SceneManager.LoadScene(SecondLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }
    public void Level3()
    {
        SceneManager.LoadScene(ThirdLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
        levelsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(settingsButton);
    }

    public void LevelSelectionButton()
    {
        levelsWindow.SetActive(true);
        settingsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(lvlButton);
    }

    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(ngButton);
    }

    public void Credit()
    {
        SceneManager.LoadScene(CreditsLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        escapeM.action.started += EscapeM;
    }
    private void EscapeM(InputAction.CallbackContext obj)
    {
        CloseSettingsButton();
    }
}
