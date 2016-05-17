using UnityEngine;
using System.Collections;

public class Brute : Enemy {

    private int attackRange;
    private int attackTimer;
    public int AttackDuration;

    public override void Start()
    {
        base.Start();
    }

    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        _health = 3;
        _xVelocity = -1.0f;
        _currentLane = 0;
        Lane = _currentLane;
        currentState = State.Tracking;
        nextStateOnBeat = currentState;
        attackRange = 2;
        attackTimer = -1;
        transform.position = new Vector3(transform.position.x, 1.0f * (-3 + Lane), transform.position.z);
        minimumDistance = 1.0f;
        knockBackVector = new Vector3(-4, 0, 0);
    }

    // Update is called once per frame
    public override void Update()
    { 
        base.Update();
        if (attackTimer > 0)
        {
            attackTimer--;
            if (attackTimer <= 0)
            {
                attackTimer = -1;
                this.gameObject.transform.GetChild(0).GetComponent<Attack>().attacking = false;
                dtKnockBack = 0.0f;
                knockingBack = true;
                //Debug.Log("ATTACK STOP"); 
            }
        }
        if(knockingBack)
        {
            Knockback();
        }
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        dtKnockBack = 0.0f;
        knockingBack = true;
    }

    public override void DoAttackPattern()
    {
        base.DoAttackPattern();
    }

    protected override void AttackActive()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Attack>().attacking = true;        
        attackTimer = AttackDuration;
        currentState = State.Idle;
        nextStateOnBeat = State.Tracking;
    }
}
