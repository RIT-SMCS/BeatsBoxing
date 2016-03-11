using UnityEngine;
using System.Collections;

public class Enemy : LineActor {

	// Use this for initialization
	void Awake () {
        _health = 1;
        _xVelocity = 1.0f;
        _currentLane = 0;
        Lane = _currentLane;
	}
	
	// Update is called once per frame
	void Update () {
	
	}  

    protected override void DoAttackPattern()
    {

    }
}
