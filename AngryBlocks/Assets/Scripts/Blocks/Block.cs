using System;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private int _count;
    private Text _countText;
    private static Progress _progress;
    private static AudioSource _bounceSound;

    private void Awake()
    {
        _countText = transform.GetComponentInChildren<Text>();
        _progress = GameObject.Find("ExtraBallProgress").GetComponent<Progress>();
        _bounceSound = GameObject.Find("BounceSound").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -15)
        {
            GameController.DecreaseBlockCount();
            Destroy(gameObject);
        }   
    }

    public void SetStartingCount(int count)
    {
        _count = count;
        _countText.text = _count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D target) 
    {
        if (target.collider.name == "Ball" && _count > 0)
        {
            _count--;
            GameController._camera.Shake(0.1f);
            _countText.text = _count.ToString();
            _bounceSound.Play();
            if (_count == 0)
            {
                GameController.DecreaseBlockCount();
                Destroy(gameObject);
                GameController._camera.Shake(0.15f);
                GameObject.Find("ExtraBallProgress").GetComponent<Progress>().IncreaseCurrentWidth();
            }
        }
    }
}
