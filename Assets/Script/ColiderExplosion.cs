using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] SphereCollider explosionInpact;
    bool explosion = false;
    GameObject[] camera;
    GameObject[] player;
    GameObject thePlayer;
    [SerializeField]PickUpBox pickUpBox;
    bool showScript = false;
    [SerializeField] bool isAnEnemy;
    CameraShake shaking;
    [SerializeField] GameObject timerTextPrefab;


    private void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject script in camera)
        {
            shaking = script.GetComponent<CameraShake>();
        }
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject script2 in player)
        {
            if (script2.GetComponent<PickUpBox>() == true)
            {
                showScript = true;
                pickUpBox = script2.GetComponent<PickUpBox>();
                thePlayer = script2;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "BobBullet")
        {
            transform.gameObject.tag = "BobBullet";

            ShowFloatingText();
            Invoke("Explosion", 2f);
        }

    }

    void ShowFloatingText()
    {
        var countdown = Instantiate(timerTextPrefab, transform.position + new Vector3(0, 1, 0), thePlayer.transform.rotation);
        countdown.transform.position = transform.position + new Vector3(0, 1, 0);// recupere le game object vers le timer pour le repositionner sur ses co
        TimeToExplose timeToExplose = countdown.GetComponent<TimeToExplose>();
        timeToExplose.AssigneGameObjectToFollow(gameObject);
    }

    private void Explosion()
    {
        Instantiate(explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
        shaking.Shaker(0.3f, 0.5f);
        explosionInpact.enabled = true;
        explosion = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (explosion)
        {
            if (other.gameObject.tag != "Player")
            {
                Instantiate(explosionParticle, other.transform.position, other.transform.rotation);
                shaking.Shaker(0.3f, 0.5f);
                Destroy(other.gameObject);
            }
            Instantiate(explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
            shaking.Shaker(0.3f, 0.5f);
            if (showScript == true && isAnEnemy == true)
            {
                pickUpBox.ActualisateNbOfBob();
            }
            Destroy(gameObject);
        }

    }
}
