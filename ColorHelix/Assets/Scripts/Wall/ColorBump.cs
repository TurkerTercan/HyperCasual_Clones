using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColorBump : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _color;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _color = GameController.Instance.colors[Random.Range(0, GameController.Instance.colors.Length)];
        _meshRenderer.material.color = _color;
    }

    public Color GetColor()
    {
        return _color;
    }
}
