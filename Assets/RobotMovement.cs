using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{

    public List<Transform> points = new List<Transform>();
    public float speed;

    int currentPoint = 0;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[0].position;
    }

    // Update is called once per frame
    void Update()
    {


        bool reached = Vector2.Distance(transform.position, points[currentPoint].position) <= .1f;
        if (reached) currentPoint++;


        Vector3 goal = points[currentPoint].position - transform.position;
        
        transform.Translate(goal.normalized * speed * Time.deltaTime);


        

    }
}
