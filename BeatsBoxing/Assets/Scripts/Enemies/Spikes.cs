using UnityEngine;
using System.Collections;

public class Spikes : Enemy {

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = int.MaxValue;
        _xVelocity = -1.0f;
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
