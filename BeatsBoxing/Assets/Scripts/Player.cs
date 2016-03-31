using UnityEngine;
using System.Collections;
using System;

public class Player : LaneActor
{
	private int attackTimer; 

	void Awake()
    {
        XVelocity = -1.0f;
        Health = 5;
        Lane = 5;
		attackTimer = -1; 
    }

    public override void Update()
    {
        //PLAYER CONTROL HANDLING
		if (Input.GetKeyDown(KeyCode.W))
        {
            Lane++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Lane--;
        }
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			DoAttackPattern (); 
			//Debug.Log("ATTACK!"); 
		}

		//Keep the attack from persisting forever
		if (attackTimer > 0) 
		{
			attackTimer--; 
			if(attackTimer <= 0)
			{
				attackTimer = -1; 
				this.gameObject.transform.GetChild (0).GetComponent<Attack> ().attacking = false; 
				//Debug.Log("ATTACK STOP"); 
			}
		}
    }

    public override void DoAttackPattern()
    {
		this.gameObject.transform.GetChild (0).GetComponent<Attack> ().attacking = true; 
		attackTimer = 42; 
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;        
        ScoreManager.Combo = 0;
    }
}
