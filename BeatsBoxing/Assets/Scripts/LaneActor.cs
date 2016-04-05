using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SpriteRenderer), typeof (Collider2D))]
public abstract class LaneActor : MonoBehaviour {
    [SerializeField]
    public const int MAX_LANES = 5;

    protected int _health;
    protected int _currentLane;
    protected float _xVelocity;

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
        set { _currentLane = value;
            if (_currentLane < 0) { _currentLane = 0; }
            else if (_currentLane >= MAX_LANES) { _currentLane = MAX_LANES-1; }
            //set the y value of the transform
            transform.position = new Vector3(transform.position.x, 1.0f * (-3 + Lane), transform.position.z);
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


    // Use this for initialization
    void Awake () {
        _renderer = this.GetComponent<SpriteRenderer>();
        _xVelocity = 0.0f;
        Lane = -10;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        this.transform.position += _xVelocity*Vector3.right * Time.deltaTime;
	}

    public abstract void DoAttackPattern();
}
