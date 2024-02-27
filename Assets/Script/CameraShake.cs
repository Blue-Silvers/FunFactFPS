using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    Vector3 originalPos;
    private Vector2 turn;
    [SerializeField] float sensitivity = .5f;
    [SerializeField] GameObject player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalPos = transform.localPosition;
    }
    void Update()
    {
        if (Time.timeScale == 1)
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            player.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
            transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
        }
    }



    public IEnumerator Shake (float duration, float magnitude)
    {
        

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            if (Time.timeScale == 1)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);


                elapsed += Time.deltaTime;
            }

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public void Shaker(float shakeDuration, float shakeMagnitude)
    {
        StartCoroutine(Shake(shakeDuration, shakeMagnitude));
    }
}
