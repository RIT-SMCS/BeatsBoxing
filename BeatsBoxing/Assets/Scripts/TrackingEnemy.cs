using UnityEngine;
using System.Collections;

public class TrackingEnemy : Enemy {

	// Use this for initialization
	void Awake () {
        _health = 1;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
        startX = 3.0f;
        currentState = State.Tracking;
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}

    protected override void DoAttackPattern()
    {
        if (currentState == State.Tracking)
        {

        }
    }
}
