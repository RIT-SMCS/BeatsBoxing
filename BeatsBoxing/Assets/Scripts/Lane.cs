using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class Lane : MonoBehaviour {
    public bool IsParent = true;
	// Update is called once per frame
	void Update () {
        float xVel = -1.0f - (ScoreManager.SpeedScale - 1.0f);
        if (IsParent)
        {
            transform.position += new Vector3(xVel, 0.0f, 0.0f) * Time.deltaTime;
        }
        if (transform.position.x < -Camera.main.orthographicSize)
        {
            //transform.position = new Vector3(Screen.width, transform.position.y, transform.position.z);
            transform.Translate(Camera.main.orthographicSize * 2.0f, 0.0f, 0.0f);
        }
    }
}
