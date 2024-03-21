using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TimeToExplose : MonoBehaviour
{
     double lifeTime = 2;
    [SerializeField]TextMeshPro time;
    GameObject[] player;
    GameObject thePlayer;
    GameObject blockToFollow;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gameObjectPlayer in player)
        {
            if (gameObjectPlayer.GetComponent<PickUpBox>() == true)
            {
                thePlayer = gameObjectPlayer as GameObject;
            }

        }
    }

    
    void Update()
    {
        transform.LookAt(thePlayer.transform, Vector3.up);

        if(blockToFollow != null)
        {
            transform.position = blockToFollow.transform.position + new Vector3(0,2,0);
        }

       lifeTime -= Time.deltaTime;
        Math.Round((Decimal)lifeTime, 3);
        time.text = lifeTime.ToString("F3");
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void AssigneGameObjectToFollow(GameObject gameObject)
    {
        blockToFollow = gameObject;
    }
}
