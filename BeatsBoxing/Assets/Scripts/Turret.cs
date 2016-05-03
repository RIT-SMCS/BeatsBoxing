using UnityEngine;
using System.Collections;
using System;

public class Turret : Enemy {

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = 1;
        _xVelocity = -0.5f;
        _currentLane = 0;
        Lane = _currentLane;
        bullet = null;
        //_movementScale = 0.25f;
        minimumDistance = 6.0f;
        minimumEnemyFollowDistance = 0.5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //DoAttackPattern();
    }

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();
        if (bullet == null)
        {
            bullet = Instantiate(Resources.Load("BulletPrefab")) as GameObject;
            bullet.transform.position = this.transform.position;
        }
    }

    protected override void AttackActive()
    {
        ShootBullet();
        currentState = State.Moving;
        nextStateOnBeat = State.AttackStartup;
    }
}
