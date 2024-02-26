using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float speedRotation, speedMovement;

    bool doMoove, doRotate;
    int rdMoove, rdRotateTime;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        doMoove = false;
        doRotate = false;
        gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (doRotate)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(0, speedRotation, 0);

            }
            else
            {
                rdRotateTime = Random.Range(0, 10000);
                if (rdRotateTime < 3)
                {
                    Invoke("Rotating", (float)rdRotateTime);
                    doRotate = true;
                }
            }

            if (doMoove)
            {
                Vector3 movement = new Vector3(transform.forward.x * speedMovement * Time.deltaTime, 0, transform.forward.z * speedMovement * Time.deltaTime);
                rigidbody.MovePosition(transform.position + movement);
            }
            else
            {
                rdMoove = Random.Range(0, 1000);
                if (rdMoove < 7)
                {
                    Invoke("Mooving", (float)rdMoove);
                    doMoove = true;
                }
            }
        }
    }

    private void Mooving()
    {
        doMoove = false;
    }

    private void Rotating()
    {
        doRotate = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            doMoove = false;
            Invoke("Rotating", 1f);
            doRotate = true;
        }
    }
}
