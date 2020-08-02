using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = -1.5f;

    // Update is called once per frame
    void Update()
    {
        if (target.position.y - distance > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - distance, transform.position.z);
        }
    }
}
