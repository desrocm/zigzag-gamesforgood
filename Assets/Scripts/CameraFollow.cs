using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject ball;
    //distance between camera and ball
    Vector3 offset;
    //rate which the camera will follow the ball so its not a shaky cam
    public float lerpRate;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        //get the distance at start
        offset = ball.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Follow();
        }
            
    }
    
    void Follow()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = ball.transform.position - offset;
        //lerp set the rate of movement of position
        pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
        transform.position = pos;
    }
}
