using UnityEngine;
using System.Collections;

public class Gairaffe2Controller : MonoBehaviour {

    bool hasJumped = false;
    float rotateSpeed = 250;
    float moveSpeed = 2.08f;

    private Vector2 initialSpot = new Vector2(8.2f, -1.0f);
    private Quaternion initialRotation = new Quaternion(0f, 0f, 0f, 0f);

    // Use this for initialization
    void Start()
    {
        moveSpeed *= Time.deltaTime;
        rotateSpeed *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //rotate clockwise
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        //rotate counterclockwise
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward * -rotateSpeed);
        }
        //jump if not on ground
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown("/") && !hasJumped)
        {
            rigid.AddForce(Vector2.up * 20);
            rigid.velocity += Vector2.up * 8.5f;
            hasJumped = true;
            StartCoroutine(Jumped());
        }
        //go left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition += new Vector3(-moveSpeed, 0, 0);
            //GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-1, 0));
        }
        //go right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1, 0));
            transform.localPosition += new Vector3(moveSpeed, 0, 0);
        }
    }

    IEnumerator Jumped()
    {
        yield return new WaitForSeconds(1.5f);
        hasJumped = false;
    }

    void ResetPlace()
    {
        gameObject.transform.position = initialSpot;
        gameObject.transform.rotation = initialRotation;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    void ResetComplete()
    {
        ResetPlace();
    }
}

