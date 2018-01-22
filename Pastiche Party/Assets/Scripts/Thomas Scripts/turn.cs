using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour {

    public float turnRate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, turnRate * Time.deltaTime);
    }
}
