using UnityEngine;

public class HelixCylinder : MonoBehaviour
{
    [Header("Variables")] 
    
    [Range(15, 50)][SerializeField] private float percentageRotation = 25;
    
    private GameObject _helix;
    private void Awake()
    {
        _helix = GameObject.Find("Helix");
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0, _helix.gameObject.transform.eulerAngles.z % percentageRotation);
    }
}
