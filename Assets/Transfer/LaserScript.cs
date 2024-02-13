using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    private LineRenderer line;
    private Transform laserSpawnPoint;
    [SerializeField] private float maxLenght;

    [SerializeField] private float damagePerTick;
    [SerializeField] private float maxCharge;
    private float actualCharge;
    [SerializeField] private float surcharge;
    [SerializeField] private float maxLaserTime;
    [SerializeField] private float laserTime;
    private float actualLaserTime;
    bool matainShoot = false;
    [SerializeField] private float shootTiming;


    [SerializeField] HealthBar ammoBar;


    [SerializeField] private ParticleSystem startParticle;
    [SerializeField] private ParticleSystem endParticle;

    [SerializeField] private bool upgrade1 = false;
    [SerializeField] private float damageUp1;
    [SerializeField] private bool upgrade2 = false;
    [SerializeField] private float damageUp2;
    [SerializeField] private bool upgrade3 = false;
    [SerializeField] private float damageUp3;

    private void Start()
    {
        ammoBar.SetMaxHealth(maxCharge);

        line = GetComponent<LineRenderer>();
        laserSpawnPoint = GetComponent<Transform>();
        line.enabled = false;

        startParticle.Stop();
        endParticle.Stop();

        actualCharge = maxCharge;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            actualLaserTime = -10;
            matainShoot = true;
            line.enabled = true;
            startParticle.Play();
            endParticle.Play();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)) 
        { 
            line.enabled = false;
            matainShoot = false;
            startParticle.Stop();
            endParticle.Stop();
            line.SetPosition(0, laserSpawnPoint.position);
            line.SetPosition(1, laserSpawnPoint.position);
        }
    }

    private void FixedUpdate()
    {
        if (matainShoot == true)
        {
            if (actualCharge <= 0)
            {
                actualCharge = 0;
                ammoBar.SetHealth(actualCharge);
                Surcharge();
            }
            else
            {
                actualCharge -= surcharge;

                ammoBar.SetHealth(actualCharge);

                if (actualLaserTime == -10)
                {
                    Surcharge();
                    Invoke("ShootAgain", 0.1f);
                }
                else if (actualLaserTime <= 0)
                {
                    Surcharge();
                    Invoke("ShootAgain", shootTiming);
                }
                else
                {
                 actualLaserTime -= laserTime;
                }
                
            }


        }
        else 
        { 
            return; 
        }

        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, maxLenght);
        Vector3 hitPosition = cast ? hit.point : laserSpawnPoint.position + laserSpawnPoint.forward * maxLenght;
        line.SetPosition(0, laserSpawnPoint.position);
        line.SetPosition(1, hitPosition);  


        endParticle.transform.position = hitPosition;


        if(line.enabled == true && cast && hit.collider.TryGetComponent(out EnemyScript enemyScript))
        {
            enemyScript.LaserDamage(damagePerTick);
        }
        
        if (line.enabled == true && cast && hit.collider.TryGetComponent(out EniroDestruction eniroDestruction))
        {
            eniroDestruction.LaserDamage(damagePerTick);
        }


        if (upgrade3 == true)
        {
            damagePerTick = damageUp3;
        }
        else if (upgrade2 == true)
        {
            damagePerTick = damageUp2;
        }
        else if (upgrade1 == true)
        {
            damagePerTick = damageUp1;
        }
        else 
        {
            return;
        }
    }

    private void Surcharge()
    {
        line.enabled = false;
        startParticle.Stop();
        endParticle.Stop();
        line.SetPosition(0, laserSpawnPoint.position);
        line.SetPosition(1, laserSpawnPoint.position);
    }


    public void Reload(float reloading)
    {
        actualCharge += reloading;

        if(actualCharge > maxCharge)
        {
            actualCharge = maxCharge;
        }

        ammoBar.SetHealth(actualCharge);
    }

    private void ShootAgain()
    {
        if(matainShoot == true)
        {
            actualLaserTime = maxLaserTime;
            line.enabled = true;
            startParticle.Play();
            endParticle.Play();
        }
    }

    public void Upgrade()
    {
        if (upgrade2 == true)
        { 
            upgrade3 = true; 
        }
        else if (upgrade1 == true)
        {
            upgrade2 = true;
        }
        else if (upgrade1 != true)
        {
            upgrade1 = true;
        }
    }
}