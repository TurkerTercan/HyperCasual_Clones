using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    public Transform cameraContainer;
    private float rotateSemiAmount = 4;
    private float shakeAmount;

    private Vector3 startingLocalPos;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        if (shakeAmount > 0.01f)
        {
            Vector3 localPos = startingLocalPos;
            localPos.x += shakeAmount * Random.Range(3, 5);
            localPos.y += shakeAmount * Random.Range(3, 5);
            transform.localPosition = localPos;
            shakeAmount *= 0.9f;
        }
    }

    public void Shake(float shake)
    {
        shakeAmount = Mathf.Min(shake, shakeAmount + shake * 0.1f);
    }

    public void RotateCameraToSide()
    {
        StartCoroutine(RotateCameraToSideRoutine());
    }

    public void RotateCameraToFront()
    {
        StartCoroutine(RotateCameraToFrontRoutine());
    }

    IEnumerator RotateCameraToSideRoutine()
    {
        int frames = 20;
        float increment = rotateSemiAmount / (float) frames;
        for (int i = 0; i < frames; i++)
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, increment);
            yield return null;
        }
        yield break;
    }
    
    IEnumerator RotateCameraToFrontRoutine()
    {
        int frames = 60;
        float increment = rotateSemiAmount / (float) frames;
        for (int i = 0; i < frames; i++)
        {
            cameraContainer.RotateAround(Vector3.zero, Vector3.up, -increment);
            yield return null;
        }
        cameraContainer.localEulerAngles = new Vector3(0,0,0);
        yield break;
    }
}
