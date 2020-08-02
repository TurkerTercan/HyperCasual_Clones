using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wall : MonoBehaviour
{
    private GameObject wallFragment;
    private GameObject wall1, wall2;
    private GameObject perfectStar;

    private float rotationZ;
    private float rotationZMax = 180f;
    private bool _smallWall = false;

    private void Awake()
    {
        wallFragment = Resources.Load("WallFragment") as GameObject;
        perfectStar = Resources.Load("PerfectStar") as GameObject;
    }

    private void Start()
    {
        SpawnWallFragments();
    }

    private void SpawnWallFragments()
    {
        wall1 = new GameObject();
        wall2 = new GameObject();
        wall1.name = "Wall1";
        wall2.name = "Wall2";
        wall1.tag = "Hit";
        wall2.tag = "Fail";

        wall1.transform.SetParent(transform);
        wall2.transform.SetParent(transform);

        _smallWall = Random.value <= (float)GameController.Percentage / 100;

        if (_smallWall)
            rotationZMax = 90;
        else
            rotationZMax = 180;
        
        for (int i = 0; i < 100; i++)
        {
            GameObject wallF = Instantiate(wallFragment, transform.position, Quaternion.Euler(0,0, rotationZ));
            rotationZ += 3.6f;

            if (rotationZ < rotationZMax)
            {
                wallF.transform.SetParent(wall1.transform);
                wallF.gameObject.tag = "Hit";
            }
            else
            {
                wallF.transform.SetParent(wall2.transform);
                wallF.transform.tag = "Fail";
            }
        }
        
        wall1.transform.position = Vector3.zero;
        wall2.transform.position = Vector3.zero;
        
        wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);

        if (!_smallWall)
        {
            wall1.AddComponent<BoxCollider>();
            wall1.GetComponent<BoxCollider>().center = new Vector3(-0.5f, 0, wall1.transform.parent.position.z);
            wall1.GetComponent<BoxCollider>().size = new Vector3(1, 2, 0.12f);
            wall2.AddComponent<BoxCollider>();
            wall2.GetComponent<BoxCollider>().center = new Vector3(0.5f, 0, wall2.transform.parent.position.z);
            wall2.GetComponent<BoxCollider>().size = new Vector3(1, 2, 0.12f);
        
            AddStar(wall1.transform.GetChild(25).gameObject);
        }
        else
        {
            wall1.AddComponent<BoxCollider>();
            wall1.GetComponent<BoxCollider>().center = new Vector3(-0.5f, 0.5f, wall1.transform.parent.position.z);
            wall1.GetComponent<BoxCollider>().size = new Vector3(1, 1, 0.12f);
            wall2.AddComponent<BoxCollider>();
            wall2.GetComponent<BoxCollider>().center = new Vector3(0.5f, 0, wall2.transform.parent.position.z);
            wall2.GetComponent<BoxCollider>().size = new Vector3(1, 2, 0.12f);
            wall2.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
            wall2.transform.GetChild(0).GetComponent<BoxCollider>().center = new Vector3(-0.5f,0.5f, 0);
            wall2.transform.GetChild(0).GetComponent<BoxCollider>().size = new Vector3(1,1, 0.12f);
        
            AddStar(wall1.transform.GetChild(14).gameObject);
        }
        
    }

    private void AddStar(GameObject wallFragmentChild)
    {
        GameObject star = Instantiate(perfectStar, transform.position, Quaternion.identity);
        star.transform.SetParent(wallFragmentChild.transform);
        star.transform.localPosition = new Vector3(0.0f, 0.75f, -0.12f);
        star.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    void Update()
    {
        
    }
}
