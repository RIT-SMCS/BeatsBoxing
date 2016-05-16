﻿using UnityEngine;
using System.Collections;

public class Wall : Enemy {

    public override void Start()
    {
        base.Start();
    }

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = int.MaxValue;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
		currentState = State.Moving;
		nextStateOnBeat = State.Moving;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (_currentLane >= MAX_LANES - 1) { _currentLane = MAX_LANES - 2; }
    }

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();
    }

	protected override void AttackActive ()
	{
		throw new System.NotImplementedException ();
	}
}
