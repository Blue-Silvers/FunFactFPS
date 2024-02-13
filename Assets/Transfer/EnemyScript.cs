using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float maxLife;
    private float actualLife;

    [SerializeField] private Transform laserSpawnPoint;
    [SerializeField] private float maxLenght;
    [SerializeField] private float maxLenghtView;

    [SerializeField] Transform playerPos;

    bool fight;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject bulletPrefab, explosionParticle;
    [SerializeField] float bulletSpeed = 50;
    bool shoot;
    [SerializeField] int nbMaxAmmo;
    int nbAmmo;

    Animator animator;
    private void Start()
    {
        actualLife = maxLife;
        nbAmmo = nbMaxAmmo;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLenght);
        bool view = Physics.Raycast(ray, out RaycastHit show, maxLenghtView);


        if (fight)
        {
            if (cast && hit.collider.tag == "Player")
            {
                animator.SetBool("View", true);
                if(nbAmmo <= 0)
                {
                    animator.SetBool("NeedAmmo", true);
                    Invoke("Reload", 2.20f);
                }
                else if(shoot == true)
                {
                    shoot = false;
                    var bullet = Instantiate(bulletPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = laserSpawnPoint.forward * bulletSpeed;
                }
                else
                {
                    Invoke("ShootAgain", 0.15f);
                }
            }
            else
            {
                animator.SetBool("View", false);
                animator.SetBool("Walk", true);
                agent.SetDestination(playerPos.position);
                fight = true;
            }
        }
        else if (view && show.collider.tag == "Player")
        {
            fight = true;
        }
        else
        {
            animator.SetBool("View", false);
            animator.SetBool("Walk", false);
        }
    }

    public void LaserDamage(float damage)
    {
        actualLife -= damage;
        fight = true;
        if (actualLife < 0)
        {
            animator.SetBool("Death", true);
            Instantiate(explosionParticle, laserSpawnPoint.position, laserSpawnPoint.rotation);
            Destroy(gameObject, 2f);
        }
    }

    private void ShootAgain()
    {
        shoot = true;
    }

    private void Reload()
    {
        nbAmmo = nbMaxAmmo;
    }
}
