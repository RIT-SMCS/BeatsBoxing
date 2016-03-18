using UnityEngine;
using System.Collections;
using System;

public class Player : LineActor
{
    void Awake()
    {
        XVelocity = 0.0f;

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
