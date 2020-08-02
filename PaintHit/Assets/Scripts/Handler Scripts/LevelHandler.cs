using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;
    public static int ballsCount;
    public static int totalCircles;
    public static Color currentColor;
    
    void Awake()
    {
        if (PlayerPrefs.GetInt("firstTime1", 0) == 0)
        {
            PlayerPrefs.SetInt("firstTime1", 1);
            PlayerPrefs.SetInt("C_Level", 1);
        }
        UpgradeLevel();
    }

    public void UpgradeLevel()
    {
        currentLevel = PlayerPrefs.GetInt("C_Level", 1);

        switch (currentLevel)
        {
            case 1:
                ballsCount = 3;
                totalCircles = 2;
                break;
            case 2:
                ballsCount = 3;
                totalCircles = 3;
                break;
            case 3:
                ballsCount = 3;
                totalCircles = 4;
                break;
            case 4:
                ballsCount = 3;
                totalCircles = 5;
                break;
            case 5:
                ballsCount = 3;
                totalCircles = 5;
                break;
            case 6:
                ballsCount = 3;
                totalCircles = 5;
                break;
        }

        if (currentLevel >= 8 && currentLevel <= 12)
        {
            ballsCount = 4;
            totalCircles = 5;
        }

        if (currentLevel >= 12 && currentLevel <= 20)
        {
            ballsCount = 4;
            totalCircles = 6;
            BallHandler.RotationTime = 120;
            BallHandler.RotationTime = 2;
        }

        if (currentLevel >= 21)
        {
            ballsCount = 5;
            totalCircles = 6;
            BallHandler.RotationTime = 1;
            BallHandler.RotationSpeed = 140;
        }
    }
    
    public static void MakeHurdles(int count)
    {
        GameObject gameObject = GameObject.Find("Circle" + BallHandler.CurrentCircleNo);
        int[] array = new int[count];
        bool[] isCount = new bool[24];
        int x = 0;
        for (int i = 0; i < count; i++)
        {
            int temp = Random.Range(0, 24);
            if (!isCount[temp])
            {
                array[x] = temp;
                isCount[temp] = true;
                x++;
            }
            else
                count++;
        }
        foreach (var t in array)
        {
            var meshRenderer = gameObject.transform.GetChild(t).gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
            meshRenderer.material.color = currentColor;
            gameObject.transform.GetChild(t).gameObject.tag = "red";
        }
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt("C_Level", 1);
        ballsCount = 3;
        totalCircles = 2;
        currentLevel = 1;
    }
}
