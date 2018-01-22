using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour {

    private Vector2 initialSpot = new Vector2(0, 3.2f);
    private Quaternion initialRotation = new Quaternion(0f,0f,0f,0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
