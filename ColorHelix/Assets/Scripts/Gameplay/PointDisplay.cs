using UnityEngine;

public class PointDisplay : MonoBehaviour
{
    private TextMesh _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
    }

    public void SetText(string text)
    {
        _textMesh.text = text;
        _textMesh.color = Color.white;
    }

    private void LateUpdate()
    {
        var position = transform.position;
        position = new Vector3(position.x, position.y, Ball.GetZAxis());
        transform.position = position;
        Destroy(gameObject, 1.2f);
    }
}
