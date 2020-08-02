using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private string[] colorNames = {"Cyan","Yellow","Pink","Magenta"};
    [SerializeField] private Color[] _colors;

    public string currentColor;
    // Start is called before the first frame update
    void Awake()
    {
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetRandomColor();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            _rigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ColorChanger"))
        {
            SetRandomColor();
            Destroy(other.gameObject);
            return;
        }
        if (!other.CompareTag(currentColor))
        {
            Debug.Log("GameOver!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void SetRandomColor()
    {
        int index = Random.Range(0, 4);
        currentColor = colorNames[index];
        _spriteRenderer.color = _colors[index];
    }
}
