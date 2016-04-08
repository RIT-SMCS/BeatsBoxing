using UnityEngine;
using System.Collections;

public class Turret : Enemy {

    //private 

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = 1;
        _xVelocity = -0.5f;
        _currentLane = 0;
        Lane = _currentLane;
        currentState = State.Attacking;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();

    }
}
