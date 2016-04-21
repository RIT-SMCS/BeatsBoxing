using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SpriteRenderer), typeof (Collider2D))]
public abstract class LaneActor : MonoBehaviour {
    [SerializeField]
    public const int MAX_LANES = 5;

    public const float LANEHEIGHT = 1.0f;

    protected int _health;
    protected int _currentLane;
    protected float _xVelocity;

    protected float dtLaneSwitch = 0;
    protected bool switchingLanes = false;
    protected float laneSwitchDuration = 0.25f;


    protected float dtKnockBack = 0;
    protected bool knockingBack = false;

    public Vector3 baseScale;
    protected Vector3 startingPos;

    protected Vector3 knockBackVector = Vector3.zero;

    protected float _movementScale = 1.0f;

    protected int directionToMove = 0;

    protected bool _isReady = false;

    private SpriteRenderer _renderer;

    public Color Color
    {
        get { return _renderer.color; }
        set { _renderer.color = value;
            //call SpriteRenderer.Color
        }
    }

    //Controls the lane that the actor occupies
    public int Lane
    {
        get { return _currentLane; }
        set {
            if (_isReady)
            {
                directionToMove = value - _currentLane;
                _currentLane = value;
                if (_currentLane < 0)
                {
                    _currentLane = 0;
                    directionToMove = 0;
                }
                else if (_currentLane >= MAX_LANES)
                {
                    _currentLane = MAX_LANES - 1;
                    directionToMove = 0;
                }
                //set the y value of the transform
                

                if (directionToMove != 0)
                {
                    switchingLanes = true;
                    startingPos = transform.position;
                    dtLaneSwitch = 0.0f;
                }
                else
                {
                    switchingLanes = false;
                }
            } else {
                _currentLane = value;
                if (_currentLane < 0)
                {
                    _currentLane = 0;
                }
                else if (_currentLane >= MAX_LANES)
                {
                    _currentLane = MAX_LANES - 1;
                }

                transform.position = new Vector3(transform.position.x, 1.0f * (-3 + _currentLane), transform.position.z);
            }
        }
    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    public float XVelocity
    {
        get { return _xVelocity; }
        set { _xVelocity = value; }
    }


    public void Knockback()
    {
        dtKnockBack += Time.deltaTime;
        float duration = 1.0f;
        float t = Mathf.Min(dtKnockBack, duration) / duration;
        transform.position = Vector3.Lerp(startingPos, startingPos - knockBackVector, t);
        float scaleFactor = 1 + Mathf.Sin(t * Mathf.PI);
        transform.localScale = baseScale * scaleFactor;
        if (dtKnockBack > duration)
        {
            knockingBack = false;
            this.startingPos = transform.position;
        }

    }

    public void ReadyUp()
    {
        _isReady = true;
    }

    public void LaneSwitch()
    {
        dtLaneSwitch += Time.deltaTime;
        float t = Mathf.Min(dtLaneSwitch, laneSwitchDuration) / laneSwitchDuration;
        Vector3 displacement = new Vector3(0, directionToMove, 0);
        transform.position = Vector3.Lerp(startingPos, startingPos + displacement * LANEHEIGHT, t);
        if (dtLaneSwitch > laneSwitchDuration)
        {
            switchingLanes = false;
            this.startingPos = transform.position;
            directionToMove = 0;
        }

    }

    // Use this for initialization
    public virtual void Awake () {
        _renderer = this.GetComponent<SpriteRenderer>();
        _xVelocity = 0.0f;

        baseScale = transform.localScale;
        startingPos = transform.position;


    }
	
	// Update is called once per frame
	public virtual void Update () {
        this.transform.position += _movementScale * new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
    }

    public abstract void DoAttackPattern();
}
