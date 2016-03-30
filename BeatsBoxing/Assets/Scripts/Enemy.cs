using UnityEngine;
using System.Collections;

public class Enemy : LaneActor {

    [SerializeField]
    private float startX;    

    public float StartX
    {
        get { return startX; }
		set { startX = value;}
    }

	// Use this for initialization
	void Awake () {
        _health = 1;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
        startX = 3.0f;
	}
	
	// Update is called once per frame
	public override void Update () {
        //this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
        base.Update();
    }  

    public override void DoAttackPattern()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.tag == "Player")
        {           
            col.gameObject.GetComponent<Player>().TakeDamage(1);                       
        }
    }

	public void TakeDamage(int damage)
	{
		this.Health -= damage;        
		ScoreManager.Combo += 1;
		Debug.Log ("Damaged"); 
		Debug.Log (this.Health); 
	}
}
