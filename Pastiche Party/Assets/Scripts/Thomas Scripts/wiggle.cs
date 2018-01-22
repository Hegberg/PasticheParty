using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiggle : MonoBehaviour {

    public float time;
    public float degree;
    public bool booly;

    void Start()
    {
        StartCoroutine(Wiggle(booly));
    }

    IEnumerator Wiggle(bool bol)
    {
        if(bol) { 
            transform.Rotate(0, 0, degree);
        } else
        {
            transform.Rotate(0, 0, -degree);
        }

        yield return new WaitForSeconds(time);

        if (bol)
        {
            transform.Rotate(0, 0, -degree);
        }
        else
        {
            transform.Rotate(0, 0, degree);
        }

        StartCoroutine(Wiggle(!bol));

    }
}
