using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EniroDestruction : MonoBehaviour
{
    [SerializeField] private float maxLife;
    private float actualLife;

    [SerializeField] GameObject explosionParticle;
    private void Start()
    {
        actualLife = maxLife;
    }
    public void LaserDamage(float damage)
    {
        actualLife -= damage;
        if (actualLife < 0)
        {
            Instantiate(explosionParticle);
            Destroy(gameObject);
        }
    }
}
