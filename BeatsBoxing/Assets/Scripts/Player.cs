using UnityEngine;
using System.Collections;
using System;

public class Player : LaneActor
{
    void Awake()
    {
        XVelocity = -1.0f;
        Health = 5;
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Lane++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Lane--;
        }
    }

    protected override void DoAttackPattern()
    {
        
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;
        Debug.Log("Hark! I have been struck! My health is now " + this.Health + "!");
        ScoreManager.Combo = 0;
    }
}
