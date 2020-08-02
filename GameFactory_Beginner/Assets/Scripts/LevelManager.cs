
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartScene()
    {
        StartCoroutine(RestartSceneCoroutine());
    }


    private IEnumerator RestartSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoopLevels()
    {
        int temp = SceneManager.GetActiveScene().buildIndex;
        if (temp == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(0);
    }
}
