using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TimeToExplose : MonoBehaviour
{
     double lifeTime = 2;
    [SerializeField]TextMeshPro time;
    GameObject[] player;
    GameObject thePlayer;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject script2 in player)
        {
            if (script2.GetComponent<PickUpBox>() == true)
            {
                thePlayer = script2 as GameObject;
            }

        }
    }

    
    void Update()
    {
        /*Vector3 direction = thePlayer.transform.position + new Vector3(180, 0, 0);
        direction.Normalize();
        float angle = 270 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(1, 1, 1) * (float)angle);*/
        transform.LookAt(thePlayer.transform.position);
        //transform.rotation = Quaternion.Euler(new Vector3(0, 1, 0));


       lifeTime -= Time.deltaTime;
        Math.Round((Decimal)lifeTime, 3);
        time.text = lifeTime.ToString("F3");
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
