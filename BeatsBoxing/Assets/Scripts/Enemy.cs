using UnityEngine;
using System.Collections;

public class Enemy : LineActor {

    [SerializeField]
    private float startX;
    private bool hasCollided;

    public float StartX
    {
        get { return startX; }
        set { startX = value; }
    }

	// Use this for initialization
	void Awake () {
        _health = 1;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
        startX = 3.0f;
        hasCollided = false;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
    }  

    protected override void DoAttackPattern()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!hasCollided)
            {
                col.gameObject.GetComponent<Player>().TakeDamage(1);
                hasCollided = true;
            }
        }
    }    
}
