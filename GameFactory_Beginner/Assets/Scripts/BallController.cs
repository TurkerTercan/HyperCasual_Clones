using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    [SerializeField] private float _jumpFeedBack = 2.0f;
    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right);
        } else if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!_isGrounded)
        {
            return;
        }

        _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
    }

    private void Move(Vector3 direction)
    {
        _rigidbody.AddForce(direction * _moveSpeed, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        
        CheckEnemyCollision(collision);

    }

    private void CheckEnemyCollision(Collision collision)
    {
        bool hasCollidedWithEnemy = collision.collider.GetComponent<Enemy>();
        if (!hasCollidedWithEnemy)
            return;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            bool isOnTopOfEnemy = enemy != null;
            if (isOnTopOfEnemy)
            {
                enemy.Die();
                _rigidbody.AddForce(Vector3.up * _jumpFeedBack, ForceMode.Impulse);
                return;
            }
        }
        Die();
    }

    

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectible collectible = other.GetComponent<Collectible>();
        bool isCollectible = collectible != null;

        if (isCollectible)
        {
            collectible.Collect();
        }

        if (other.gameObject.CompareTag("DieZone"))
            Die();
    }

    private void Die()
    {
        FindObjectOfType<LevelManager>().RestartScene();
        GetComponent<MeshRenderer>().enabled = false;
    }

   
}
