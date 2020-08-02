using UnityEngine;

public class Circle1 : MonoBehaviour
{
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "easetype", iTween.EaseType.easeInCirc, "time", 0.2));
    }
    void Update()
    {
        transform.Rotate(Vector3.up * (Time.deltaTime * BallHandler.RotationSpeed));
    }
}
