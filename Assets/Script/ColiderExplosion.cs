using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    [SerializeField] SphereCollider explosionInpact;
    bool explosion = false;
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
                Destroy(other.gameObject);
            }
            Instantiate(explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }

    }
}
