using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{ 
    public GameObject[] blocks;
    public List<GameObject> levels;
    [SerializeField] private float newGravity = -17;
    public Text ballsCountText;

    private GameObject level1;
    private GameObject level2;

    private Vector2 level1Pos;
    private Vector2 level2Pos;

    private ShotCountText _shotCountText;
    private GameObject ballsContainer;
    public GameObject gameOver;

    private static int _blockCount;
    public static CameraTransitions _camera;
    public static int ShotCount;
    public int ballsCount;
    public int score;

    private bool firstShot; 
    

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraTransitions>();
        _shotCountText = GameObject.Find("Canvas ShotCount").GetComponent<ShotCountText>();
        ballsContainer = GameObject.Find("BallsContainer");
    }

    // Start is called before the first frame update
    private void Start()
    {
        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        PlayerPrefs.DeleteKey("Level");
        ballsCountText.text = ballsCount.ToString();
        Physics2D.gravity = new Vector2(0, newGravity);
        SpawnLevel();
        GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", true);
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (ballsContainer.transform.childCount == 0 && ShotCount == 4)
        {
            gameOver.SetActive(true);
            GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", false);
        }

        if (ShotCount > 2)
            firstShot = false;
        else
            firstShot = true;
        
        CheckBlocks();
    }

    private void SpawnNewLevel(int numberLevel1, int numberLevel2, int min, int max)
    {
        ShotCount = 1;
        
        level1Pos = new Vector2(2.75f, 1);
        level2Pos = new Vector2(2.75f, -3.4f);

        level1 = levels[numberLevel1];
        level2 = levels[numberLevel2];

        Instantiate(level1, level1Pos, Quaternion.identity);
        Instantiate(level2, level2Pos, Quaternion.identity);
        
        SetBlocksCount(min, max);

    }

    private void SpawnLevel()
    {
        if (PlayerPrefs.GetInt("Level") == 0)
            SpawnNewLevel(0, 17, 3, 5);
        
        if (PlayerPrefs.GetInt("Level") == 1)
            SpawnNewLevel(1, 18, 3, 5);
        
        if (PlayerPrefs.GetInt("Level") == 2)
            SpawnNewLevel(2, 19, 3, 6);
        
        if (PlayerPrefs.GetInt("Level") == 3)
            SpawnNewLevel(5, 20, 4, 7);
        
        if (PlayerPrefs.GetInt("Level") == 4)
            SpawnNewLevel(12, 28, 5, 8);
        
        if (PlayerPrefs.GetInt("Level") == 5)
            SpawnNewLevel(14, 29, 7, 10);
        
        if (PlayerPrefs.GetInt("Level") == 6)
            SpawnNewLevel(15, 30, 6, 12);
        
        if (PlayerPrefs.GetInt("Level") == 7)
            SpawnNewLevel(16, 31, 9, 15);
        
        if (PlayerPrefs.GetInt("Level") == 7)
            SpawnNewLevel(16, 31, 9, 15);
        
        if (PlayerPrefs.GetInt("Level") > 7)
            SpawnNewLevel(Random.Range(0,17), Random.Range(17, 40), 9, 15);
    }
    

    private void SetBlocksCount(int min, int max)
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
        _blockCount = blocks.Length;
        foreach (var t in blocks)
        {
            int count = Random.Range(min, max);
            t.GetComponent<Block>().SetStartingCount(count);
        }
    }

    public void CheckBlocks()
    {
        if (_blockCount < 1)
        {
            if (firstShot)
                score += 5 * PlayerPrefs.GetInt("Level");
            else
                score += 3 * PlayerPrefs.GetInt("Level");
            if (ballsCount >= PlayerPrefs.GetInt("BallsCount", 5))
                PlayerPrefs.SetInt("BallsCount", ballsCount);
            
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            foreach (var ball in Shoot.GetBalls())
                Destroy(ball);
            SpawnLevel();
        }
    }

    public static void DecreaseBlockCount()
    {
        _blockCount--;
    }

    public void CheckShotCount()
    {
        if (ShotCount == 1 || ShotCount == 2)
        {
            _shotCountText.SetTopText("SHOT");
            _shotCountText.SetBottomText(ShotCount + "/3");
            _shotCountText.Flash();
        }
        else if (ShotCount == 3)
        {
            _shotCountText.SetTopText("FINAL");
            _shotCountText.SetBottomText("SHOT!");
            _shotCountText.Flash();
        }
    }
}
