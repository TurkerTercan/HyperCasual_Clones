using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Progress : MonoBehaviour
{
    public RectTransform extraBallInner;

    private GameController _gameController;

    private float currentWidth, addWidth, totalWidth;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        extraBallInner.sizeDelta = new Vector2(0.2f,1);
        currentWidth = 0.2f;
        totalWidth = 3.20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWidth >= totalWidth)
        {
            _gameController.ballsCount++;
            _gameController.ballsCountText.text = _gameController.ballsCount.ToString();
            currentWidth = 0.2f;
        }

        if (currentWidth >= addWidth)
        {
            addWidth += 0.2f;
            extraBallInner.sizeDelta = new Vector2(addWidth, 1);
        }
        else
        {
            addWidth = currentWidth;
        }
    }

    public void IncreaseCurrentWidth()
    {
        float addRandom = Random.Range(0.2f, 0.6f);
        currentWidth = addRandom + 0.2f + currentWidth % 4f;
    }
}
