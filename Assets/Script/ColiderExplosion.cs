using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] SphereCollider explosionInpact;
    bool explosion = false;
    GameObject[] camera;
    CameraShake shaking;


    private void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject script in camera)
        {
            shaking = script.GetComponent<CameraShake>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            transform.gameObject.tag = "Bullet";
            Invoke("Explosion", 2f);
        }

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
            Destroy(gameObject);
        }

    }
}
