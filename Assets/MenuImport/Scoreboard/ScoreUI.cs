using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public HighscoreTable rowUi;
    public ScoreManager scoreManager;

    public void ScoreBoard()
    {

        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<HighscoreTable>();
            row.rank.text = (i + 1).ToString();
            row.palyerName.text = scores[i].palyerName;
            row.score.text = scores[i].score.ToString();
        }
    }

}
