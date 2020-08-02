
using UnityEngine;

public class Circle2 : MonoBehaviour
{
    void Start()
    {
        iTween.MoveTo(base.gameObject, iTween.Hash("y", 0, "easetype", iTween.EaseType.easeInOutQuad, 
            "time", 0.6, "OnComplete", "RotateCircle"));
    }

    private void RotateCircle()
    {
        iTween.RotateBy(gameObject, iTween.Hash("y", 0.8f, "time", BallHandler.RotationTime, 
            "easeType", iTween.EaseType.easeInOutQuad, "loopType", iTween.LoopType.pingPong, "delay", 0.4));
    }
}
