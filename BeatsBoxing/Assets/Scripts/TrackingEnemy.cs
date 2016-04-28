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
        currentState = State.Tracking;
        attackRange = 2;
        transform.position = new Vector3(transform.position.x, 1.0f * (-3 + Lane), transform.position.z);
    }
	
	// Update is called once per frame
	public override void Update () {
        if(currentState == State.Tracking)
        {
            int playerLane = player.GetComponent<Player>().Lane;
            if (!switchingLanes && this.Lane != playerLane)
            {
                if(playerLane > Lane)
                {
                    Lane++;
                }
                else
                {
                    Lane--;
                }
            }
        }
        if(Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
        {
            this.currentState = State.Attacking;
        }

        if (switchingLanes)
        {
            LaneSwitch();
        }

        base.Update();
	}

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();        
    }
}
