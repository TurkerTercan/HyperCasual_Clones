using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [Header("Variables")] 
    [Range(0.55f, 0.65f)] [SerializeField] private float height = 0.58f;
    [SerializeField] private float speed = 6;
    
    private bool _move;
    private bool _isRising;
    private static float z;
    private float _lerpAmount;
    private bool _gameOver;
    private bool _display;
    

    private static Color _currentColor;
    private MeshRenderer _meshRenderer;
    private CameraFollow _cameraFollow;
    private SpriteRenderer _spriteRenderer;
    private SphereCollider _sphereCollider;
    private GameObject _pointDisplayPrefab;

    private AudioSource _failSound, _hitSound, _levelCompleteSound;


    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _pointDisplayPrefab = Resources.Load("PointDisplay") as GameObject;

        _failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        _hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        _levelCompleteSound = GameObject.Find("LevelCompleteSound").GetComponent<AudioSource>();
    }


    void Start()
    {
        _move = false;
        SetColor(GameController.Instance.hitColor);
        
    }

    private void Update()
    {
        if (Touch.IsPressing() && !_gameOver)
            _move = true;
        UpdateColor();
        _display = false;
    }

    private void FixedUpdate()
    {
        if (_move)
            z += speed * Time.fixedDeltaTime;
        
        transform.position = new Vector3(0, height, z);
    }

    public static float GetZAxis()
    {
        return z;
    }

    public static Color SetColor(Color color)
    {
        return _currentColor = color;
    }
    
    public static Color GetColor()
    {
        return _currentColor;
    }

    private void UpdateColor()
    {
        _meshRenderer.sharedMaterial.color = _currentColor;
        if (_isRising)
        {
            _currentColor = Color.Lerp(_meshRenderer.material.color,GameObject.FindGameObjectWithTag("ColorBump").GetComponent<ColorBump>().GetColor(), _lerpAmount);
            _lerpAmount += Time.deltaTime;
        }

        if (_lerpAmount >= 1) 
            _isRising = false;
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.CompareTag("Hit"))
        {
            Destroy(target.gameObject);
            GameController.Score += PlayerPrefs.GetInt("Level", 1);
            if (!_display)
            {
                _display = true;
                GameObject display = Instantiate(_pointDisplayPrefab, transform.position, Quaternion.identity);
                display.GetComponent<PointDisplay>().SetText("+" + PlayerPrefs.GetInt("Level", 1));
                _hitSound.Play();
            }
            
        }
        else if (target.CompareTag("Star"))
        {
            Destroy(target.transform.parent.parent.gameObject);
            int temp = Random.Range(1, 5);
            GameController.Score += PlayerPrefs.GetInt("Level", 1) * temp;
            if (!_display)
            {
                _display = true;
                GameObject display = Instantiate(_pointDisplayPrefab, transform.position, Quaternion.identity);
                display.GetComponent<PointDisplay>().SetText("PERFECT +" + (PlayerPrefs.GetInt("Level", 1) * temp));
                _hitSound.Play();
            }
        }
        
        else if (target.CompareTag("Fail"))
        {
            _failSound.Play();
            StartCoroutine(GameOver());
        }
        else if (target.CompareTag("ColorBump"))
        {
            _lerpAmount = 0;
            _isRising = true;
        }
        
        else if (target.CompareTag("FinishLine"))
        {
            _levelCompleteSound.Play();
            StartCoroutine(PlayNewLevel());
        }
    }

    IEnumerator GameOver()
    {
        Helix._movable = false;
        _gameOver = true;
        _spriteRenderer.color = _currentColor;
        var spriteRendererTransform = _spriteRenderer.transform;
        spriteRendererTransform.position = new Vector3(0, 0.7f, z - 0.05f);
        spriteRendererTransform.eulerAngles = new Vector3( 0, 0, Random.value * 360);
        _spriteRenderer.enabled = true;
        
        _meshRenderer.enabled = false;
        _move = false;
        _sphereCollider.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        
        for (float f = 1f; f >= -0.02f; f -= 0.02f)
        {
            Color c = _spriteRenderer.color;
            c.a = f;
            _spriteRenderer.color = c;
            yield return new WaitForSeconds(0.02f);
        }
        _cameraFollow.Flash();
        yield return new WaitForSeconds(2f);
        
        _meshRenderer.enabled = true;
        _sphereCollider.enabled = true;
        
        PlayerPrefs.SetInt("Level", 1);
        GameController.Score = 0;
        
        GameController.Instance.GenerateLevel();
        z = 0;
        _gameOver = false;
        Helix._movable = true;
    }

    IEnumerator PlayNewLevel()
    {
        _cameraFollow.enabled = false;
        _cameraFollow.Flash();
        yield return new WaitForSeconds(1.5f);
        _move = false;
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        _cameraFollow.enabled = true;
        z = 0;
        GameController.Instance.GenerateLevel();
    }
}
