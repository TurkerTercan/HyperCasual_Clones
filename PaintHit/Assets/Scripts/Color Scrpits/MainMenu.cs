using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image backGround;
    public Sprite[] sprites;
    public BallHandler handler;
    public GameObject pauseScreen;
    public GameObject mainMenuScreen;
    public GameObject failScreen;

    void Start()
    {
        backGround.sprite = sprites[Random.Range(0, 5)];
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void unPauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void ReturnHome()
    {
        var arr = GameObject.FindGameObjectsWithTag("circle");
        foreach (var element in arr)
        {
            Destroy(element);
        }
        handler.enabled = false;
        mainMenuScreen.SetActive(true);
        pauseScreen.SetActive(false);
        failScreen.SetActive(false);
        Time.timeScale = 1;
    }

}
