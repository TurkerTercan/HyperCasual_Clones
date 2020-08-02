using UnityEngine;

public class Ball : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -15)
        {
            Destroy(gameObject);
        }   
    }
}
