﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player2Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "apple")
        {
            //SceneManager.LoadScene("VictoryScene");
            GameControllerScript.control.Player2Scored();
        }
    }
}
