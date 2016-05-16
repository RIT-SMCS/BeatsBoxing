using UnityEngine;
using System.Collections;

public abstract class Enemy : LaneActor {

    protected enum State { AttackStartup, AttackActive, Moving, Idle, Tracking};
        
    protected float startX;
    [SerializeField]
    protected State currentState;
    protected State nextStateOnBeat;
    protected Player player;
    [SerializeField]
    protected EnemyTable eTable;

    private float bubbleDuration = 2.0f;

    protected float minimumDistance = -1000.0f;
    protected float minimumEnemyFollowDistance = -1000.0f;
    protected int attackBeatCooldown = 1;

    protected int currentStateBeatCount = 0;

    protected int idleBeats = 2;

    protected GameObject bullet;

    private Collider2D collider;
    public float BubbleDuration
    {
        get { return bubbleDuration; }
        set { bubbleDuration = Mathf.Max(0.1f, value);
            StartCoroutine(PrepForAttack(bubbleDuration));
        }
    }

    public float StartX
    {
        get { return startX; }
        set { startX = value; }
    }
    public EnemyTable ETable
    {
        get { return eTable; }
        set { eTable = value; }
    }

    public virtual void Start()
    {
        eTable = new EnemyTable();        
        eTable.Add("BasicEnemyPrefab", 4.0f);
        eTable.Add("TrackingEnemyPrefab", 3.0f);
        eTable.Add("TurretEnemyPrefab", 2.0f);
        eTable.Add("SpikesPrefab", 1.0f);
        eTable.Add("WallPrefab", 1.0f);
    }

	// Use this for initialization
	public override void Awake () {
        base.Awake();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentState = State.Moving;
        nextStateOnBeat = currentState;

        collider = this.transform.GetComponent<Collider2D>();

        BeatManager.Instance.ExecuteOnBeat += UpdateStateOnBeat;        

        //Debug.Log(eTable.EnemyTypes);        
    }
	
	// Update is called once per frame
	public override void Update () {
        //this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
        base.Update();
        
        switch (currentState)
        {
            case State.Idle: Idle();
                break;
            case State.Moving: Move();
                break;
            case State.Tracking: Track();
                break;
            case State.AttackStartup: AttackStartup();
                break;
            case State.AttackActive: AttackActive();
                break;
        }
    }

    protected virtual void Idle() { }
    protected virtual void Move() {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position+1.01f*Vector3.left * transform.GetComponent<Collider2D>().bounds.extents.x, Vector2.left);
        RaycastHit2D hit = Physics2D.Raycast((transform.position + new Vector3(collider.offset.x, collider.offset.y, 0.0f)) + 1.01f * Vector3.left * collider.bounds.extents.x, Vector2.left);
        if (hit.transform != null)
        {
            //Debug.DrawLine(transform.position, hit.transform.position, Color.blue);
            //Debug.Log(this.transform.name + " HIT " + hit.transform.name);
            
        }
        if (transform.position.x - player.transform.position.x > minimumDistance && ( hit.transform == null || hit.transform.tag != "Enemy" || hit.distance >= minimumEnemyFollowDistance))
        {
            this.transform.position += _movementScale * new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
        }
        else
        {
            nextStateOnBeat = State.AttackStartup;
        }
              
    }
    protected virtual void Track() {
        Move();
        
        
        if (BeatManager.Instance.IsOnBeat)// && (upHit.transform == null || upHit.distance <= LaneActor.LANEHEIGHT))
        {
            float sign = Mathf.Sign(player.Lane - this.Lane);
            RaycastHit2D hit = Physics2D.Raycast((transform.position+ new Vector3(collider.offset.x, collider.offset.y, 0.0f)) + sign * 1.01f * Vector3.up * collider.bounds.extents.y, sign*Vector2.up);
            if (hit.transform == null || hit.distance > LaneActor.LANEHEIGHT)
            {
                this.Lane += (int)Mathf.Sign(player.Lane - this.Lane);
            }
        }
        
    }

    protected virtual void AttackStartup()
    {
        if (BeatManager.Instance.IsOnBeat)
        {
            BubbleDuration = BeatManager.Instance.TimePerBeat;
            nextStateOnBeat = State.AttackActive;
        }
    }
    protected abstract void AttackActive();

    private void UpdateStateOnBeat()
    {
        if (currentState != nextStateOnBeat)
        {
            currentState = nextStateOnBeat;
            nextStateOnBeat = currentState;
            currentStateBeatCount = 0;
        }
        else
        {
            ++currentStateBeatCount;
        }
    }


    public override void DoAttackPattern()
    {
        //Debug.Log("Attacked!");
    }

    protected IEnumerator PrepForAttack(float duration)
    {
        SetBubble(duration);
        yield return new WaitForSeconds(duration);
        DoAttackPattern();
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

    protected void ShootBullet()
    {
        if (bullet == null || true)
        {
            bullet = Instantiate(Resources.Load("BulletPrefab")) as GameObject;
            bullet.transform.position = this.transform.position;
        }
    }
}
