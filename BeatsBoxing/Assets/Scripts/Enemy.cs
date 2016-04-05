using UnityEngine;
using System.Collections;

public abstract class Enemy : LaneActor {

    //Tracking: move towards left (may also move between lanes)
    //Resting: stay static in left (may NOT move between lanes)
    //Attacking: performing an attack
    protected enum State { Attacking, Tracking, Resting};

    [SerializeField]
    protected float startX;
    [SerializeField]
    protected State currentState;
    protected GameObject player;

    //The time the enemy telegraphs its attack in seconds
    protected float attackCountdown = 1f;
    //The distance from the player that the enemy stops moving and starts attacking.
    protected float attackDistance = 1.2f;
    //The time that the attack began. Used to time cooldowns and such
    [SerializeField]
    protected float timeAttackBegan;
    //The time between one attack and another
    protected float attackCooldown = 4f;

    public float StartX
    {
        get { return startX; }
        set { startX = value; }
    }

	// Use this for initialization
	public virtual void Awake () {
        //StartCoroutine(PrepForAttack(2));
        player = GameObject.FindGameObjectWithTag("Player");
        attackCooldown = Mathf.Max(attackCountdown, attackCooldown);
        currentState = State.Tracking;
    }
	
	// Update is called once per frame
	public override void Update () {
        //this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
        switch(currentState)
        {
            case State.Tracking:
                base.Update();
                if (Vector3.Distance(this.transform.position.x * Vector3.right, player.transform.position.x * Vector3.right) < attackDistance) {
                    DoAttackPattern();
                }
                break;
            case State.Attacking:
                if (Time.realtimeSinceStartup - timeAttackBegan > attackCooldown)
                {
                    currentState = State.Resting;
                }
                break;
            case State.Resting:
                currentState = State.Tracking;
                break;
        }
    }  

    public override void DoAttackPattern()
    {
        //Debug.Log("attack");
        StartCoroutine(PrepForAttack(attackCountdown));
        currentState = State.Attacking;
        timeAttackBegan = Time.realtimeSinceStartup;
    }
    /// <summary>
    /// Create a bubble that will last seconds
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    protected IEnumerator PrepForAttack(float duration)
    {
        SetBubble(duration);
        yield return new WaitForSeconds(duration);
        //DoAttackPattern();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.tag == "Player")
        {           
            col.gameObject.GetComponent<Player>().TakeDamage(1);                       
        }
    } 
    
    public void SetBubble(float duration)
    {
        GameObject temp = Instantiate(Resources.Load("Telegraph")) as GameObject;
        temp.transform.parent = transform;
        temp.transform.localPosition = new Vector3(0, 0, 1);
        temp.GetComponent<TelegraphAttackBubble>().duration = duration;
    }   
}
