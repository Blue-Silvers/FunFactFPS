using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class KeyBoardGO : MonoBehaviour
{
    public TMP_InputField nameField;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject keyboardButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject scordboard;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ScoreUI scoreUI;
    int actualScore;

    void Start()
    {
        scordboard.SetActive(false);
        gameOver.SetActive(true);
        eventSystem.SetSelectedGameObject(keyboardButton);
        nameField.text = "";
    }


    public void ButtonA()
    {
        nameField.text = nameField.text + "a";
    }
    public void ButtonZ()
    {
        nameField.text = nameField.text + "z";
    }
    public void ButtonE()
    {
        nameField.text = nameField.text + "e";
    }
    public void ButtonR()
    {
        nameField.text = nameField.text + "r";
    }
    public void ButtonT()
    {
        nameField.text = nameField.text + "t";
    }
    public void ButtonY()
    {
        nameField.text = nameField.text + "y";
    }
    public void ButtonU()
    {
        nameField.text = nameField.text + "u";
    }
    public void ButtonI()
    {
        nameField.text = nameField.text + "i";
    }
    public void ButtonO()
    {   
        nameField.text = nameField.text + "o";
    }
    public void ButtonP()
    {
        nameField.text = nameField.text + "p";
    }
    public void ButtonQ()
    {
        nameField.text = nameField.text + "q";
    }
    public void ButtonS()
    {   
        nameField.text = nameField.text + "s";
    }
    public void ButtonD()
    {
        nameField.text = nameField.text + "d";
    }
    public void ButtonF()
    {
        nameField.text = nameField.text + "f";
    }
    public void ButtonG()
    {
        nameField.text = nameField.text + "g";
    }
    public void ButtonH()
    {
        nameField.text = nameField.text + "h";
    }
    public void ButtonJ()
    {
        nameField.text = nameField.text + "j";
    }
    public void ButtonK()
    {
        nameField.text = nameField.text + "k";
    }
    public void ButtonL()
    {
        nameField.text = nameField.text + "l";
    }
    public void ButtonM()
    {
        nameField.text = nameField.text + "m";
    }
    public void ButtonW()
    {
        nameField.text = nameField.text + "w";
    }
    public void ButtonX()
    {
        nameField.text = nameField.text + "x";
    }
    public void ButtonC()
    {
        nameField.text = nameField.text + "c";
    }
    public void ButtonV()
    {
        nameField.text = nameField.text + "v";
    }
    public void ButtonB()
    {
        nameField.text = nameField.text + "b";
    }
    public void ButtonN()
    {
    nameField.text = nameField.text + "n";
    }
    public void Button_()
    {
        nameField.text = nameField.text + "_";
    }
    public void Button6()
    {
        nameField.text = nameField.text + "-";
    }    
    public void Delette()
    {
        nameField.text = "";
    }

    public void CurrentScore(int currentScore)
    {
        actualScore = currentScore;
    }

    public void Validate()
    {
        print(actualScore);
        scoreManager.Add(new Score(nameField.text, actualScore));
        scoreUI.ScoreBoard();
        scordboard.SetActive(true);
        gameOver.SetActive(false);
        eventSystem.SetSelectedGameObject(menuButton);
    }

}
