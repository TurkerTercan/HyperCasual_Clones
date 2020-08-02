using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text scoreText;

    public Text bestScoreText;
    public Text bestScoreText2;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    

    // Update is called once per frame
    void Update()
    {
        scoreText.text = _gameController.score.ToString();
        if (_gameController.score > PlayerPrefs.GetInt("BestScore", 0))
            PlayerPrefs.SetInt("BestScore", _gameController.score);
        bestScoreText.text = "Best " + PlayerPrefs.GetInt("BestScore");
        bestScoreText2.text = "Best " + PlayerPrefs.GetInt("BestScore");
    }

    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
