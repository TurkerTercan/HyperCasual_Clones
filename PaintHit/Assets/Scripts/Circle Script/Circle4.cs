using UnityEngine;

public class Circle4 : MonoBehaviour
{
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "easetype", 
            iTween.EaseType.easeInOutQuad, "time", 0.6, "OnComplete", "RotateCircle"));
    }

    private void RotateCircle()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", 0.75f, "time", BallHandler.RotationTime, 
            "easeType", iTween.EaseType.easeInOutQuad, "loopType", iTween.LoopType.pingPong, "delay", 0.5));
    }
}
