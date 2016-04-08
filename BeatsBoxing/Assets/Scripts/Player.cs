using UnityEngine;
using System.Collections;
using System;

public class Player : LaneActor
{
	private int attackTimer; 
	public AudioClip punch; //the actual punch sound
	public AudioClip switchLane; //the switching lanes sound
	public AudioClip takeDamage; //the "oof" sound effect
	public AudioSource source; //the audioSource object that plays the sounds
	float audioVol; //volume of any given sound

	void Awake()
    {
        XVelocity = -1.0f;
        Health = 5;
        Lane = 5;
		attackTimer = -1; 

		//audio setup
		audioVol = 3.0f; 
		source = GetComponent<AudioSource> (); 
    }

    public override void Update()
    {
        //PLAYER CONTROL HANDLING
		if (Input.GetKeyDown(KeyCode.W))
        {
            Lane++;
			source.PlayOneShot (switchLane, audioVol); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Lane--;
			source.PlayOneShot (switchLane, audioVol); 
        }
		if (Input.GetKeyDown (KeyCode.Space) && attackTimer <= 0) 
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
		source.PlayOneShot (punch, audioVol); 
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;        
        ScoreManager.Combo = 0;
		source.PlayOneShot (takeDamage, audioVol); 
    }
}
