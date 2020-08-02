using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Text))]
public class BallHandler : MonoBehaviour
{
    public static float RotationSpeed = 130f;
    public static float RotationTime = 3f;
    public static int CurrentCircleNo;
    private static int _circleCount;
    
    public static Color OneColor = Color.magenta;
    
    public GameObject ball;
    public GameObject dummyBall;
    public GameObject levelComplete;
    public GameObject button;
    public GameObject failScreen;
    public GameObject startGameScreen;
    public GameObject circleEffect;
    public GameObject completeEffect;
    
    
    private int _ballsCount;
    private int _circleNo;
    private int _heartNo;
    
    private Color[] _changingColors;
    public SpriteRenderer sprite;
    public Material splashMat;
    
    private float speed = 100f;

    public Image[] balls;
    public GameObject[] hearts;

    [FormerlySerializedAs("total_balls_text")] public Text totalBallsText;
    [FormerlySerializedAs("count_balls_text")] public Text countBallsText;
    public Text levelCompleteText;

    public AudioSource completeSound;
    public AudioSource gameFailSound;
    
    private LevelHandler _levelHandler;
    private bool _gameFail = false;

    // Start is called before the first frame update
    private void Awake()
    {
        _levelHandler = FindObjectOfType<LevelHandler>();
    }
    void OnEnable()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        _circleCount = 1;
        _changingColors = ColorScript.colorArray;
        OneColor = _changingColors[0];
        sprite.color = OneColor;
        splashMat.color = OneColor;
        
        ChangeBallsCount();
        
        GameObject gameObject2 = Instantiate(Resources.Load("round" + Random.Range(1,6))) as GameObject;
        gameObject2.transform.position = new Vector3(0, 20, 23);
        gameObject2.name = "Circle" + _circleNo;

        _ballsCount = LevelHandler.ballsCount;
        CurrentCircleNo = _circleNo;
        LevelHandler.currentColor = OneColor;

        _heartNo = PlayerPrefs.GetInt("hearts");
        if (_heartNo == 0)
        {
            PlayerPrefs.SetInt("hearts", 1);
        }
        _heartNo = PlayerPrefs.GetInt("hearts");
        
        for (int i = 0; i < _heartNo; i++)
        {
            hearts[i].SetActive(true);
        }
        MakeHurdles();
    }

    public void FailGame()
    {
        gameFailSound.Play();
        _gameFail = true;
        Invoke(nameof(FailScreen), 1);
        button.SetActive(false);
        StopCircle();
    }

    void StopCircle()
    {
        GameObject o = GameObject.Find("Circle" + _circleNo);
        o.transform.GetComponent<MonoBehaviour>().enabled = false;
        if (o.GetComponent<iTween>())
            o.GetComponent<iTween>().enabled = false;
        
    }

    void FailScreen()
    {
        failScreen.SetActive(true);
    }

    public void DeleteAllCircles()
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("circle");
        foreach (var o in arr)
        {
            Destroy(o.gameObject);
        }

        _gameFail = false;
        FindObjectOfType<LevelHandler>().UpgradeLevel();
        ResetGame();
    }
    
    
    public void HeartsLow()
    {
        _heartNo--;
        PlayerPrefs.SetInt("hearts", _heartNo);
        hearts[_heartNo].SetActive(false);
    }
    
    public void HitBall()
    {
        if (_ballsCount <= 1)
        {
            StartCoroutine(HideButton());
            Invoke(nameof(MakeANewCircle), 0.4f);
        }

        _ballsCount--;

        if (_ballsCount >= 0)
            balls[_ballsCount].enabled = false;
        
        
        GameObject gameObject2 = Instantiate(ball, new Vector3(0, 0, -8), Quaternion.identity);
        gameObject2.GetComponent<MeshRenderer>().material.color = OneColor;
        gameObject2.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    void ChangeBallsCount()
    {
        _ballsCount = LevelHandler.ballsCount;
        dummyBall.GetComponent<MeshRenderer>().material.color = OneColor;

        totalBallsText.text = LevelHandler.totalCircles.ToString();
        countBallsText.text = _circleCount.ToString();
        
        foreach (var t in balls)
        {
            t.enabled = false;
        }

        for (var i = 0; i < _ballsCount; i++)
        {
            balls[i].enabled = true;
            balls[i].color = OneColor;
        }
        
    }

    private void MakeANewCircle()
    {
        if (_circleCount >= LevelHandler.totalCircles && !_gameFail)
        {
            completeSound.Play();
            StartCoroutine(LevelCompleteScreen());
        }
        else
        {
            StartCoroutine(CircleEffect());
            var array = GameObject.FindGameObjectsWithTag("circle");
            var o = GameObject.Find("Circle" + _circleNo); 
            for (int i = 0; i < 24; i++)
            {
                o.transform.GetChild(i).gameObject.SetActive(false);
            }
            o.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = OneColor;
            if (o.GetComponent<iTween>())
            {
                o.GetComponent<iTween>().enabled = false;
            }
            
            foreach (GameObject target in array)
            {
                iTween.MoveBy(target, iTween.Hash(new object[]
                {
                    "y",
                    -3f,
                    "easetype",
                    iTween.EaseType.spring,
                    "time",
                    0.5
                }));
            }
            _circleNo++;
            CurrentCircleNo = _circleNo;
        
            GameObject gameObject2 = Instantiate(Resources.Load("round" + Random.Range(1,6))) as GameObject;
            gameObject2.transform.position = new Vector3(0, 20, 23);
            gameObject2.name = "Circle" + _circleNo;
        
            _ballsCount = LevelHandler.ballsCount;

            OneColor = _changingColors[Mathf.Clamp(_circleNo % 8, 0, 7)];
            sprite.color = OneColor;
            splashMat.color = OneColor;
        
            LevelHandler.currentColor = OneColor;
            ChangeBallsCount();
            _circleCount++;
            MakeHurdles();
        }
    }

    private void MakeHurdles()
    {
        switch (_circleNo)
        {
            case 1:
                LevelHandler.MakeHurdles(1);
                break;
            case 2:
                LevelHandler.MakeHurdles(2);
                break;
            case 3:
                LevelHandler.MakeHurdles(3);
                break;
            case 4:
                LevelHandler.MakeHurdles(4);
                break;
            case 5:
                LevelHandler.MakeHurdles(5);
                break;
        }
    }

    IEnumerator HideButton()
    {
        if (!_gameFail)
        {
            button.SetActive(false);
            yield return new WaitForSeconds(1);
            button.SetActive(true);
        }
    }

    IEnumerator LevelCompleteScreen()
    {
        _gameFail = true;
        completeEffect.SetActive(true);

        GameObject oldCircle = GameObject.Find("Circle" + _circleNo);
        for (int i = 0; i < 24; i++)
        {
            oldCircle.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }

        oldCircle.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = OneColor;
        oldCircle.transform.GetComponent<MonoBehaviour>().enabled = false;
        if (oldCircle.GetComponent<iTween>())
        {
            oldCircle.GetComponent<iTween>().enabled = false;
        }
        
        button.SetActive(false);
        yield return new WaitForSeconds(2);
        levelComplete.SetActive(true);
        levelCompleteText.text = LevelHandler.currentLevel.ToString();
        yield return new WaitForSeconds(1);
        
        GameObject[] oldCircles = GameObject.FindGameObjectsWithTag("circle");
        foreach (var old in oldCircles)
        {
            Destroy(old.gameObject);
        }

        yield return new WaitForSeconds(1);
        completeEffect.SetActive(false);
        int currentLevel = PlayerPrefs.GetInt("C_Level");
        currentLevel++;
        PlayerPrefs.SetInt("C_Level", currentLevel);
        _levelHandler.UpgradeLevel();
        ResetGame();
        levelComplete.SetActive(false);
        startGameScreen.SetActive(true);
        _gameFail = false;

    }

    IEnumerator CircleEffect()
    {
        yield return new WaitForSeconds(.4f);
        circleEffect.SetActive(true);
        yield return new WaitForSeconds(.8f);
        circleEffect.SetActive(false);
    }
}
