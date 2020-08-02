using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Values")] 
    [SerializeField] private float currentYPosition = 2.2f;
    [Range(1,5)] [SerializeField] private float distance = 1.8f;
    
    private float cameraZ;
    private Animator _animator;

    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        
        cameraZ = Ball.GetZAxis() - distance;
        transform.position = new Vector3(0, currentYPosition, cameraZ);    
    }

    public void Flash()
    {
        _animator.SetTrigger("Flash");
        cameraZ = 0;
    }
}
