using UnityEngine;
using System.Collections;

public class Turret : Enemy {

    private GameObject bullet; 

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = 1;
        _xVelocity = -0.5f;
        _currentLane = 0;
        Lane = _currentLane;
        currentState = State.Attacking;
        bullet = null;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();
        if (bullet == null)
        {
            bullet = Instantiate(Resources.Load("Bullet")) as GameObject;
            bullet.transform.position = this.transform.position;
        }
    }
}
