using UnityEngine;
using System.Collections;

public abstract class Enemy : LaneActor {

    protected enum State { Attacking, Tracking};

    [SerializeField]
    protected float startX;
    protected State currentState;

    public float StartX
    {
        get { return startX; }
        set { startX = value; }
    }

	// Use this for initialization
	void Awake () {
           
	}
	
	// Update is called once per frame
	public override void Update () {
        //this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
        base.Update();
    }  

    protected override void DoAttackPattern()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.tag == "Player")
        {           
            col.gameObject.GetComponent<Player>().TakeDamage(1);                       
        }
    }    
}
