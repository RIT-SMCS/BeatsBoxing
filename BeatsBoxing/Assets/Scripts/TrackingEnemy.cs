using UnityEngine;
using System.Collections;

public class TrackingEnemy : Enemy {

    private int attackRange;

    // Use this for initialization
    public override void Awake(){
        base.Awake();
        _health = 1;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
        startX = 3.0f;
        currentState = State.Tracking;
        attackRange = 2;
    }
	
	// Update is called once per frame
	public override void Update () {
        if(currentState == State.Tracking)
        {
            this.Lane = player.GetComponent<Player>().Lane;
        }
        if(Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
        {
            this.currentState = State.Attacking;
        }
        base.Update();
	}

    protected override void DoAttackPattern()
    {
        base.DoAttackPattern();
    }
}
