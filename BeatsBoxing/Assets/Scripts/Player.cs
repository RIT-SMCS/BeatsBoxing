using UnityEngine;
using System.Collections;
using System;

public class Player : LaneActor
{
    void Awake()
    {
        XVelocity = -1.0f;

    }

    void Update()
    {
    }

    protected override void DoAttackPattern()
    {
        
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;
        Debug.Log("Hark! I have been struck! My health is now " + this.Health + "!");
    }
}
