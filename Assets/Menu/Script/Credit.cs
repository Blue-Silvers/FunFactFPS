using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    [Header("Input Manager (don't touch)")]
    [SerializeField] private InputActionReference escapeM;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.JoystickButton0))
        {
            Time.timeScale = 5.0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            LoadMainMenu();
        }
    }
    private void OnEnable()
    {
        escapeM.action.started += EscapeM;
    }
    private void EscapeM(InputAction.CallbackContext obj)
    {
        LoadMainMenu();
    }
}