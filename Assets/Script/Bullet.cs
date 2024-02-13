using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionParticle, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

}
