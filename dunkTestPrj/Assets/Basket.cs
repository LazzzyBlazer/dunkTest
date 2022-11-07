using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject ballPrf;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numbersOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;

    private void Start() 
    {
        points = new GameObject[numbersOfPoints];
        for(int i = 0; i < numbersOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 ballPosition = transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - ballPosition;
            transform.right = direction;
            for(int i = 0; i < numbersOfPoints; i++)
            {
                points[i].SetActive(true);
            }
        }
    
        if(Input.GetMouseButtonUp(0))
        {
            Shoot();
            gameObject.SetActive(false);
            for(int i = 0; i < numbersOfPoints; i++)
            {
                points[i].SetActive(false);
            }
        }

        for(int i = 0; i < numbersOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }

    void Shoot()
    {
        GameObject newBall = Instantiate(ballPrf, shotPoint.position, shotPoint.rotation);
        newBall.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
    
    Vector2 PointPosition(float t) {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
