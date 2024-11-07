using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGestion : MonoBehaviour
{
    private float basePos;
    private int goScore; 
    private void Start()
    {
        basePos = transform.position.z;
    }

    private void Update()
    {
        goScore = (int)(transform.position.z - basePos)*4;
    }

    public int GetGoScore()
    {
        return goScore;
    }
}
