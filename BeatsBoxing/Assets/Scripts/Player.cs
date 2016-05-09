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
    public int AttackDuration;

	public Sprite animation1;
	public Sprite animation2; 
	public Sprite attackAnimation; 
	public float animationTimer;
	bool animationState; 
	//Lower this number to increase the speed of the walk animation
	public float ANIMATIONSPEED;    
    
	public override void Awake()
    {

        base.Awake();

        XVelocity = -1.0f;
        Health = 5;
        Lane = 0;
		attackTimer = -1;
        laneSwitchDuration = 0.1f;

        transform.position = new Vector3(transform.position.x, 1.0f * (-3 + Lane), transform.position.z);

        knockBackVector = new Vector3(2, 0, 0);

        ReadyUp(); 


        //audio setup
        audioVol = 3.0f; 
		source = GetComponent<AudioSource> (); 
		animationTimer = 0.0f; 
		animationState = true; 
		ANIMATIONSPEED = 0.5f;
    }

    public override void Update()
    {
        

        //PLAYER CONTROL HANDLING
		if (Input.GetKeyDown(KeyCode.W) && !knockingBack && !switchingLanes)
        {
            Lane++;
            source.PlayOneShot (switchLane, audioVol); 
        }
        if (Input.GetKeyDown(KeyCode.S) && !knockingBack && !switchingLanes)
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

        if(switchingLanes)
        {
            LaneSwitch();
        }

        animationTimer += Time.deltaTime * ScoreManager.SpeedScale; 
		if (animationTimer >= 0.5f) 
		{
			animationTimer = 0.0f; 
			if(animationState)
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = animation2; 
				animationState = !animationState;
			}
			else
			{
				this.gameObject.GetComponent<SpriteRenderer>().sprite = animation1; 
				animationState = !animationState;
			}

		}
    }

    public override void DoAttackPattern()
    {
        if (!knockingBack)
        {
            this.gameObject.transform.GetChild(0).GetComponent<Attack>().attacking = true;
            attackTimer = AttackDuration; 
            source.PlayOneShot (punch, audioVol); 
			this.gameObject.GetComponent<SpriteRenderer>().sprite = attackAnimation; 
        }
		
    }

    public void TakeDamage(int damage)
    {
        this.Health -= damage; 
       
        if (this.Health == 0) {
            Application.LoadLevel(Application.levelCount - 1);
        }

        ScoreManager.Combo = 0;
        source.PlayOneShot (takeDamage, audioVol); 
        
		if (Camera.main.GetComponent<Camera> ().WorldToScreenPoint (startingPos - new Vector3 (2, 0, 0)).x >= 100)
        {
            dtKnockBack = 0.0f;
			this.knockingBack = true;
            //this.startingPos = transform.position;
            _isReady = false;
            Lane = _currentLane;
            _isReady = true;
            startingPos.y = Mathf.Round(startingPos.y);
		}
    }

   
}
