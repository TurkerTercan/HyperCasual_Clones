using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    private Image _currentTickBoxImage;
    private Image _endLevel;
    private Image  _progression;
    
    private Image[] _alwaysColoredImages = new Image[3];

    private Text _endLevelText;
    private Text _startLevelText;
    private Text _currentTickBoxText;

    [SerializeField] private Text levelCompleteMessage;

    private RectTransform _currentTickBox;
    private Color _color;
    
    private void Awake()
    {
        _alwaysColoredImages[0] = transform.GetChild(0).GetComponent<Image>();
        _alwaysColoredImages[1] = transform.GetChild(1).GetComponent<Image>();
        _alwaysColoredImages[2] = transform.GetChild(3).GetComponent<Image>();
        _endLevel = transform.GetChild(4).GetComponent<Image>();
        
        _endLevelText = _endLevel.transform.GetChild(0).GetComponent<Text>();
        _startLevelText = transform.GetChild(3).GetChild(0).GetComponent<Text>();

        _progression = transform.GetChild(2).GetChild(0).GetComponent<Image>();
        _currentTickBox = transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
        _currentTickBoxImage = _currentTickBox.GetComponent<Image>();
        _currentTickBoxText = _currentTickBox.GetChild(0).GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_progression.fillAmount != 1)
            SetProgression(Ball.GetZAxis() / GameController.Instance.GetFinishLineDistance());
        else if(_progression.fillAmount >= 1 && Ball.GetZAxis() == 0)
            SetProgression(0);
        
        UpdateColor();

        _startLevelText.text = PlayerPrefs.GetInt("Level", 1).ToString();
        _endLevelText.text = (PlayerPrefs.GetInt("Level", 1) + 1).ToString();
    }

    private void SetProgression(float percentage)
    {
        _progression.fillAmount = percentage;
        _currentTickBox.anchorMin = new Vector2(percentage, 0);
        _currentTickBox.anchorMax = _currentTickBox.anchorMin;
        _currentTickBoxText.text = Mathf.RoundToInt(percentage * 100) + "%";
    }

    private void UpdateColor()
    {
        _color = Ball.GetColor();
        if (_progression.fillAmount == 1)
        {
            _endLevel.color = _color;
            _endLevelText.color = Color.white;
            
            levelCompleteMessage.gameObject.SetActive(true);
            levelCompleteMessage.text = "Level " + PlayerPrefs.GetInt("Level", 1) + " Completed!";
        }
        else
        {
            _endLevel.color = Color.white;
            _endLevelText.color = _color;
            levelCompleteMessage.gameObject.SetActive(false);
        }

        foreach (var image in _alwaysColoredImages)
        {
            image.color = _color;
        }

        _progression.color = _color;
        _currentTickBoxImage.color = _color; 
    }
    
}
