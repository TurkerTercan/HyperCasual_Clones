using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Shoot : MonoBehaviour
{
    public float power = 2;
    private int _dots = 15;

    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 _direction;
    private Vector2 _force;
    private float _distance;


    private bool shoot, aiming;
    
    private GameObject Dots;
    private List<GameObject> _projectilesPath;

    private Rigidbody2D ballBody;
    private Camera mainCam;

    public GameObject ballPrefab;
    public GameObject ballsContainer;

    private static List<GameObject> _balls = new List<GameObject>();
    private GameController _controller;

    private void Awake()
    {
        _controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        mainCam = Camera.main;
        ballBody = ballPrefab.GetComponent<Rigidbody2D>();
        Dots = GameObject.Find("Dots");
        _projectilesPath = Dots.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        HideDots();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameController.ShotCount > 3) return;
        Aim();
        Rotate();
    }

    private void Aim()
    {
        if (shoot)
            return;
        if (Input.GetAxis("Fire1") == 1)
        {
            if (!aiming)
            {
                aiming = true;
                _startPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                _controller.CheckShotCount();
                Debug.Log(_startPos);
            }
            else
            {
                PathCalculation();
            }
        }
        else if (aiming && !shoot)
        {
            aiming = false;
            HideDots();
            StartCoroutine(ShootBalls());
        }
    }

    private Vector2 ShootForce()
    {
        return _direction * (_distance * power);
    }
    
    
    private Vector2 DotPath(Vector2 startP, Vector2 startVel, float t)
    {
        float y = startP.y + startVel.y * t + (Physics2D.gravity.y * (t * t * 0.5f));
        float x = startP.x + startVel.x * t;
        return new Vector2(x,y);
    }

    private void PathCalculation()
    {
        _endPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        _distance = Vector2.Distance(_startPos, _endPos);
        _direction = (_startPos - _endPos).normalized;
        
        Debug.DrawLine(_startPos,_endPos);

        Vector2 vel = ShootForce();
        ShowDots();
        for (int i = 0; i < _projectilesPath.Count; i++)
        {
            var t = i / 15f;
            Vector3 point = DotPath(transform.position, vel, t);
            point.z = 1;
            _projectilesPath[i].transform.localScale = new Vector3(0.3f * (1 - 0.5f*t), 0.3f * (1 - 0.5f*t), 1);
            _projectilesPath[i].transform.position = point;
        }
    }

    private void HideDots()
    {
        foreach (var t in _projectilesPath)
        {
            t.GetComponent<Renderer>().enabled = false;
        }
    }

    private void ShowDots()
    {
        foreach (var t in _projectilesPath)
        {
            t.GetComponent<Renderer>().enabled = true;
        }
    }

    private void Rotate()
    {
        Vector2 dir = GameObject.Find("dot (1)").transform.position - transform.parent.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator ShootBalls()
    {
        for (int i = 0; i < _controller.ballsCount; i++)
        {
            yield return new WaitForSeconds(0.07f);
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            ball.name = "Ball";
            ball.transform.SetParent(ballsContainer.transform);
            ballBody = ball.GetComponent<Rigidbody2D>();
            ballBody.AddForce(ShootForce(),ForceMode2D.Impulse);
            _balls.Add(ball);

            _controller.ballsCountText.text = (_controller.ballsCount - i - 1).ToString();
        }
        
        yield return new WaitForSeconds(0.5f);
        GameController.ShotCount++;
        _controller.ballsCountText.text = _controller.ballsCount.ToString();
    }

    public static List<GameObject> GetBalls()
    {
        return _balls;
    }
}
