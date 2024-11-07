using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    [SerializeField] TextMeshProUGUI currentScoreTxt, highScoreTxt;
    public ScoreManager scoreManager;
    private int goScore;
    private int enemyScore;

    [Header("life")]
    [SerializeField] GameObject lifePoint1;
    [SerializeField] GameObject lifePoint2;
    [SerializeField] GameObject lifePoint3;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] int actualLife;
    [SerializeField] KeyBoardGO gameOverScreenKeyBoard;

    void Start()
    {
        var scores = scoreManager.GetHighScores().ToArray();
        gameOverScreen.SetActive(false);
        highScoreTxt.text = "hi : \n" + scores[0].score.ToString(); ;
        currentScoreTxt.text = "score : \n" + (goScore+enemyScore).ToString();
    }

    void Update()
    {
        currentScoreTxt.text = "score : \n" + (goScore + enemyScore).ToString();
        gameOverScreenKeyBoard.CurrentScore(goScore + enemyScore);

        if (actualLife == 0)
        {
            lifePoint1.SetActive(false);
            lifePoint2.SetActive(false);
            lifePoint3.SetActive(false);
            gameOverScreen.SetActive(true);
        }
        else if (actualLife == 1)
        {
            lifePoint1.SetActive(true);
            lifePoint2.SetActive(false);
            lifePoint3.SetActive(false);
        }
        else if (actualLife == 2)
        {
            lifePoint1.SetActive(true);
            lifePoint2.SetActive(true);
            lifePoint3.SetActive(false);
        }
        else if (actualLife == 3)
        {
            lifePoint1.SetActive(true);
            lifePoint2.SetActive(true);
            lifePoint3.SetActive(true);
        }
    }

    public void AddCurrentScore(int score)
    {
        enemyScore += score;
    }

    public void AddGoScore(int score)
    {
        goScore = score;
    }


    public void SetLife(int life)
    {
        actualLife = life;
    }
}
