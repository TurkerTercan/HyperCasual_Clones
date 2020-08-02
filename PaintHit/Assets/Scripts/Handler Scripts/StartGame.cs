using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Text levelNo;
    public Text TargetText;
    
    void OnEnable()
    {
        levelNo.text = LevelHandler.currentLevel.ToString();
        TargetText.text = LevelHandler.totalCircles.ToString();
        StartCoroutine(DelayedRemoval());
    }

    IEnumerator DelayedRemoval()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
