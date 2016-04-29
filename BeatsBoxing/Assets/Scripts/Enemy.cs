using UnityEngine;
using System.Collections;

public abstract class Enemy : LaneActor {

    protected enum State { AttackStartup, AttackActive, Moving, Idle, Tracking};
        
    protected float startX;
    [SerializeField]
    protected State currentState;
    protected State nextStateOnBeat;
    protected Player player;

    private float bubbleDuration = 2.0f;

    protected float minimumDistance = -1000.0f;
    protected float minimumEnemyFollowDistance = -1000.0f;
    protected int attackBeatCooldown = 1;

    protected int currentStateBeatCount = 0;

    protected int idleBeats = 2;

    protected GameObject bullet;
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

	// Use this for initialization
	public override void Awake () {
        base.Awake();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentState = State.Moving;
        nextStateOnBeat = currentState;

        BeatManager.Instance.ExecuteOnBeat += UpdateStateOnBeat;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position+Vector3.left * transform.GetComponent<Collider2D>().bounds.extents.x, Vector2.left, minimumEnemyFollowDistance);
        if (hit.transform != null)
        {
            Debug.DrawLine(transform.position, hit.transform.position, Color.blue);
        }
        if (transform.position.x - player.transform.position.x > minimumDistance || hit.transform == null || hit.transform.tag != "Enemy" || hit.distance >= minimumEnemyFollowDistance)
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
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + Vector3.up * transform.GetComponent<Collider2D>().bounds.extents.y, Vector2.up);
        //if (hit.transform != null)
        //{
        //    Debug.DrawLine(transform.position, hit.transform.position, Color.blue);
        //    Debug.DrawRay(transform.position, Vector2.up); 
        //}
        if (BeatManager.Instance.IsOnBeat && upHit.transform == null)
        {
            this.Lane += (int)Mathf.Sign(player.Lane - this.Lane);
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
