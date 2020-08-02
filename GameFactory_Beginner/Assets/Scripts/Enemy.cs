using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _patrolRange;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _goldCount;
    [SerializeField] private float _goldSplitSpeed;
    
    private Vector3 _initialPosition;
    private Vector3 _minPatrolPosition;
    private Vector3 _maxPatrolPosition;
    private Vector3 _destinationPoint;

    private GameObject _goldPrefab;
    

    private void Awake()
    {
        _initialPosition = transform.position;
        _minPatrolPosition = _initialPosition + (Vector3.left * _patrolRange);
        _maxPatrolPosition = _initialPosition + (Vector3.right * _patrolRange);
        LoadGoldFromResources();
        SetDestination(_maxPatrolPosition);
    }

    private void SetDestination(Vector3 destination)
    {
        _destinationPoint = destination;
    }

    private void Update()
    {
        if(Math.Abs(Vector3.Distance(transform.position, _maxPatrolPosition)) < 0.1f)
        {
            SetDestination(_minPatrolPosition);
        }
        else if (Math.Abs(Vector3.Distance(transform.position, _minPatrolPosition)) < 0.1f)
        {
            SetDestination(_maxPatrolPosition);
        }
        transform.position = Vector3.MoveTowards(transform.position, _destinationPoint, Time.deltaTime * _moveSpeed);
    }

    public void Die()
    {
        for(int i = 0; i < _goldCount; i++)
        {
            GameObject temp = Instantiate(_goldPrefab, transform.position, transform.rotation);
            Vector3 randomVector = new Vector3(Random.Range(-2f, 2f), Random.Range(0, 1.5f), 0);
            temp.transform.position = Vector3.MoveTowards(temp.transform.position, temp.transform.position + randomVector, Time.deltaTime * 1000);
            //temp.GetComponent<Rigidbody>().MovePosition(temp.transform.position + randomVector);
            
        }
        

        Destroy(gameObject);
    }

    private void LoadGoldFromResources()
    {
        _goldPrefab = Resources.Load<GameObject>("Coin");
    }
}
