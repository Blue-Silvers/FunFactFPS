using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string palyerName;
    public int score;

    public Score(string palyerName, int score)
    {
        this.palyerName = palyerName;
        this.score = score;
    }
}
