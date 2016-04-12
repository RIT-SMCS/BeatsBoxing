using UnityEngine;
using System.Collections;
using System;

public class Player : LaneActor
{
    float age = 0;
    bool knockingBack = false;

    Vector3 baseScale;
    public Vector3 startingPos;
	private int attackTimer; 
	public AudioClip punch; //the actual punch sound
	public AudioClip switchLane; //the switching lanes sound
	public AudioClip takeDamage; //the "oof" sound effect
	public AudioSource source; //the audioSource object that plays the sounds
	float audioVol; //volume of any given sound
    public int AttackDuration;
    
	void Awake()
    {
        XVelocity = -1.0f;
        Health = 5;
        Lane = 5;
		attackTimer = -1;
        baseScale = transform.localScale;
        startingPos = transform.position;
		//audio setup
		audioVol = 3.0f; 
		source = GetComponent<AudioSource> (); 
    }

    public override void Update()
    {
        

        //PLAYER CONTROL HANDLING
		if (Input.GetKeyDown(KeyCode.W) && !knockingBack)
        {
            Lane++;
			source.PlayOneShot (switchLane, audioVol); 
        }
        if (Input.GetKeyDown(KeyCode.S) && !knockingBack)
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
            if (attackTimer <= 0)
            {
                attackTimer = -1;
                this.gameObject.transform.GetChild(0).GetComponent<Attack>().attacking = false;
                //Debug.Log("ATTACK STOP"); 
            }
        }

        if (knockingBack)
        {
            Knockback();
        }
    }

    public override void DoAttackPattern()
    {
        if (!knockingBack)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Attack>().attacking = true;
            attackTimer = AttackDuration; 
            source.PlayOneShot (punch, audioVol); 
        }
		
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage;        
        ScoreManager.Combo = 0;
        source.PlayOneShot (takeDamage, audioVol); 
        
		if (Camera.main.GetComponent<Camera> ().WorldToScreenPoint (startingPos - new Vector3 (2, 0, 0)).x >= 100)
        {
			age = 0.0f;
			this.knockingBack = true;
			this.startingPos = transform.position;
		}
    }

    void Knockback()
    {
        age += Time.deltaTime;
        float duration = 1.0f;
        float t = Mathf.Min(age, duration) / duration;
        transform.position = Vector3.Lerp(startingPos, startingPos - new Vector3(2 ,0, 0), t);
        float scaleFactor = 1 + Mathf.Sin(t * Mathf.PI);
        transform.localScale = baseScale * scaleFactor; 
        if (age > duration)
        {
            knockingBack = false;
			this.startingPos = transform.position;
        }
		
    }
}
