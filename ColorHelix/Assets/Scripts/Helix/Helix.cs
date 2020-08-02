using UnityEngine;

public class Helix : MonoBehaviour
{
    public static bool _movable = true;
    private float _angle;
    private float _lastDeltaAngle, _lastTouchX;
    
    [Space(2)]
    [Header("Variables")]
    [Range(0,3)]
    [SerializeField] private float slideSpeed = 1.7f;
    [Range(0, 10)]
    [SerializeField] private float durationToStop = 5f;
    
    private void Update()
    {
        if (_movable && Touch.IsPressing())
        {
            float mouseX = GetMouseX();
            _lastDeltaAngle = _lastTouchX - mouseX;
            _angle += _lastDeltaAngle * 360 * slideSpeed;
            _lastTouchX = mouseX;
        }
        else if (_lastDeltaAngle != 0)
        {
            _lastDeltaAngle -= _lastDeltaAngle * durationToStop * Time.deltaTime;
            _angle += _lastDeltaAngle * 360 * slideSpeed;
        }
        transform.eulerAngles = new Vector3(0,0,_angle);
        
    }

    private float GetMouseX()
    {
        return Input.mousePosition.x / Screen.width;
    }
}
