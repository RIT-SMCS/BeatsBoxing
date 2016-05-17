using UnityEngine;
using System.Collections;
using System;

public class BasicEnemy : Enemy {

    public override void Start()
    {
        base.Start();
    }
    // Use this for initialization
    public override void Awake() {
        base.Awake();
        _health = 1;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;        
        currentState = State.Moving;
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();
    }

    protected override void AttackStartup()
    {
        currentState = State.Moving;
    }

    protected override void AttackActive()
    {
        currentState = State.Moving;
    }
}
