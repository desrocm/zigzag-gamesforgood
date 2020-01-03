using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BallController : MonoBehaviour
{
    public GameObject particle;

    //Serializefield to make private show up in editor to change it
    [SerializeField]
    public float speed;
    bool started;
    bool gameOver;
    //access to the balls rigidbody
    Rigidbody rb;

    void Awake()
    {
        //get the rigidbody component that is attatched to the ball. Now with rb we can change any property of the rbody
        rb = GetComponent<Rigidbody>();
    }



    // Start is called before the first frame update
    void Start()
    {
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;

                GameManager.instance.StartGame();
            }
        }


        //shows which direction and where the raycast is going
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        //creating a ray to detect other object or platform from ball
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            rb.velocity = new Vector3(0, -25f, 0);
            gameOver = true;

            GameManager.instance.GameOver();

            Camera.main.GetComponent<CameraFollow>().gameOver = true;
        }


        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }
    void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Diamond")
        {
            GameObject part = Instantiate(particle, col.gameObject.transform.position, Quaternion.identity) as GameObject;

            Destroy(col.gameObject);
            Destroy(part, 1f);

        }
    }
    public void speedUps()
    {
        InvokeRepeating("speedUp", 5f, 5f);
    }
    void speedUp()
    {
        speed = speed + 1;
    }

}
