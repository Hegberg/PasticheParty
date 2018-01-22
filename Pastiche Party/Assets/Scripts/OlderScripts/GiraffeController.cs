using UnityEngine;
using System.Collections;

public class GiraffeController : MonoBehaviour {

    bool hasJumped = false;
    float rotateSpeed = 250;
    float moveSpeed = 2.08f;

    private Vector2 initialSpot = new Vector2(-8.2f, -1.0f);
    private Quaternion initialRotation = new Quaternion(0f, 0f, 0f, 0f);

    // Use this for initialization
    void Start () {
        moveSpeed *= Time.deltaTime;
        rotateSpeed *= Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        //rotate clockwise
        if (Input.GetKey("w"))
        {
            transform.Rotate(Vector3.forward * -rotateSpeed);
        }
        //rotate counterclockwise
        else if (Input.GetKey("s"))
        {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
        //jump if not on ground
        //jumping seems currently pointless
        //float distToGround = GetComponent<PolygonCollider2D>().bounds.extents.y;
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        //if (Input.GetKeyDown("space") && Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        if (Input.GetKeyDown("space") && !hasJumped)
        {
            rigid.AddForce(Vector2.up * 20);
            rigid.velocity += Vector2.up * 8.5f;
            hasJumped = true;
            StartCoroutine(Jumped());
        }
        
        //go left
        if (Input.GetKey("a"))
        {
            transform.localPosition += new Vector3(-moveSpeed, 0, 0);
        }
        //go right
        else if (Input.GetKey("d"))
        {
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
