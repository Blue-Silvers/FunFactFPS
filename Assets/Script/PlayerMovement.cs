using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;

    bool isRunning = false;
    private Animator animator;
    private Vector2 input;
    Rigidbody rigidbody;

    private bool jump = false, isGrounded = true;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float _jumpStrength = 8f;
    [SerializeField] private float _backstabStrength = 8f;

    [SerializeField] Camera cam;
    [SerializeField] CameraShake shaking;
    [SerializeField] Vector2 cameraShakeValue;
    [SerializeField] int fovValue;
    int aiming = 0;


    [SerializeField] float maxLife;
    [SerializeField] int maxAmmo, actualAmmo;
    [SerializeField] HealthBar ammoBar;

    [SerializeField] GameObject bulletPrefab/*, explosionParticle*/;
    [SerializeField] float bulletSpeed = 50;
    bool shoot;
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private bool upgrade1 = false;
    [SerializeField] private float damageUp1;
    [SerializeField] private bool upgrade2 = false;
    [SerializeField] private float damageUp2;
    [SerializeField] private bool upgrade3 = false;
    [SerializeField] private float damageUp3;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        ammoBar.SetMaxHealth(maxAmmo);
        actualAmmo = maxAmmo;
        shoot = true;
    }

    private void Update()
    {
        if(Time.timeScale == 1)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
               aiming = 1;
            }   
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                aiming = 2;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(actualAmmo > 0 && shoot == true)
                {
                    shoot = false;
                    animator.SetTrigger("Shoot");
                    actualAmmo -= 1;
                    ammoBar.SetHealth(actualAmmo);
                    var bullet = Instantiate(bulletPrefab, rocketSpawnPoint.position, rocketSpawnPoint.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = rocketSpawnPoint.forward * bulletSpeed;
                    rigidbody.AddForce(cam.transform.forward * - _backstabStrength, ForceMode.Impulse);
                    shaking.Shaker(cameraShakeValue.x, cameraShakeValue.y);
                    if(actualAmmo > 0)
                    {
                        animator.SetBool("Ammo", true);
                    }
                    else
                    {
                        animator.SetBool("Ammo", false);
                        shoot = true;
                    }
                }
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
                isRunning = true;

            }
            else
            {
                speed = walkSpeed;
                isRunning = false;
            }

            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
                isGrounded = false;
            }
        }
        
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rigidbody.AddForce(Vector2.up * _jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        Vector3 Mmovement = new Vector3(transform.right.x * input.x * speed * Time.deltaTime, 0, transform.right.z * input.x * speed * Time.deltaTime);
        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + movement + Mmovement);


        if (aiming == 1)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue - fovValue * 30 / 100, 2f);
        }
        else if (aiming == 2)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 2f);
            if (cam.fieldOfView == fovValue)
            {
                aiming = 0;
            }
        }
        else
        {
            if (input.sqrMagnitude != 0)
            {
                if (isRunning == true)
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 20 / 100, 0.5f);
                }
                else
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 10 / 100, 0.5f);
                }
            }
            else
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 0.5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    public void Reload(int reloading)
    {
        actualAmmo += reloading;

        if (actualAmmo > maxAmmo)
        {
            actualAmmo = maxAmmo;
        }

        ammoBar.SetHealth(actualAmmo);
    }

    public void CanShoot()
    {
        shoot = true;
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
