using UnityEngine;
using System.Collections;
using System;

public class Lane : MonoBehaviour {
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < 0.0f)
        {
            transform.position = new Vector3(Screen.width, transform.position.y, transform.position.z);
        }
    }
}
