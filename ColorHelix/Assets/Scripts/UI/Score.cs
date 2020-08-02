using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _scoreText;
    private Text _bestScoreText;

    private void Awake()
    {
        _scoreText = transform.GetChild(1).GetComponent<Text>();
        _bestScoreText = transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (Ball.GetZAxis() == 0)
        {
            _bestScoreText.gameObject.SetActive(true);
            _scoreText.gameObject.SetActive(false);
        }
        else
        {
            _bestScoreText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
        }

        _scoreText.text = GameController.Score.ToString();
        if (GameController.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameController.Score);
        }

        _bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
