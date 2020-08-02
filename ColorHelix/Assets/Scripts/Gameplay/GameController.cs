using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static int Score;
    
    private GameObject _helix;
    private GameObject _finishLine;
    private GameObject[] _walls2;
    
    [Header("Wall Spawner")]
    [SerializeField] private float startWallPosZ = 7;
    [SerializeField] [Range(5, 10)] private float distanceBetweenWalls = 7;
    [Space(10)] 
    

    public Color[] colors;
    [HideInInspector] public Color hitColor, failColor;
    
    public static int Percentage;
    private bool _colorBump;
    private float _temp;
    private int wallsSpawnNumber = 11;
    

    private void Awake()
    {
        Instance = this;
        _helix = GameObject.Find("Helix");
        _finishLine = GameObject.Find("FinishLine");
        _temp = startWallPosZ;
        GenerateColors();
        
        PlayerPrefs.SetInt("Level", 1);

        Score = PlayerPrefs.GetInt("Score", 0);
    }

    private void Start()
    {
        GenerateLevel();
    }

    void Update()
    {
        
    }

    private void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];
        do
        { 
            failColor = colors[Random.Range(0, colors.Length)];
        } while (failColor == hitColor);
        
        Ball.SetColor(hitColor);
    }

    private void SpawnWalls()
    {
        for (int i = 0; i < wallsSpawnNumber; i++)
        {
            GameObject wall;
            if ((Random.value <= 0.4 && !_colorBump) || (i == wallsSpawnNumber - 1 && !_colorBump))
            {
                _colorBump = true;
                wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
            }
            else if (Random.value <= 0.15)
            {
                wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
            } 
            else
            {
                wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
            }
            wall.transform.SetParent(_helix.transform);
            wall.transform.localPosition = new Vector3(0, 0, startWallPosZ);
            float randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotation));
            startWallPosZ += distanceBetweenWalls;
        }

        _finishLine.transform.position = new Vector3(0, 0.03f, startWallPosZ);
    }

    public void GenerateLevel()
    {
        GenerateColors();
        int level = PlayerPrefs.GetInt("Level");
        wallsSpawnNumber = 10 + level / 2;
        Percentage = level * 3;
        startWallPosZ = _temp;
        DeleteWalls();
        _colorBump = false;
        SpawnWalls();
    }

    private void DeleteWalls()
    {
        _walls2 = GameObject.FindGameObjectsWithTag("Fail");
        for (int i = 0; i < _walls2.Length; i++)
        {
            Destroy(_walls2[i].transform.parent.gameObject);
        }
        Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
    }

    public float GetFinishLineDistance()
    {
        return _finishLine.transform.position.z;
    }
}
