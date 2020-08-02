using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (gameObject.CompareTag("Hit"))
        {
            GameObject colorBump = GameObject.FindGameObjectWithTag("ColorBump");
            if (transform.position.z > colorBump.transform.position.z)
                GameController.Instance.hitColor = colorBump.GetComponent<ColorBump>().GetColor();
            _meshRenderer.material.color = GameController.Instance.hitColor;
        }
        else if(gameObject.CompareTag("Fail"))
        {
            if (GameController.Instance.failColor == GameController.Instance.hitColor)
                GameController.Instance.failColor =
                    GameController.Instance.colors[Random.Range(0, GameController.Instance.colors.Length)];
            _meshRenderer.material.color = GameController.Instance.failColor;
        }
    }
}
