using UnityEngine;
using System.Collections;

[RequireComponent(typeof (SpriteRenderer), typeof (Collider2D))]
public abstract class LineActor : MonoBehaviour {
    [SerializeField]
    private const int MAX_LANES = 5;

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
            else if (_currentLane > MAX_LANES) { _currentLane = MAX_LANES; }
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
        Lane = _currentLane;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(_xVelocity, 0.0f, 0.0f) * Time.deltaTime;
	}

    protected abstract void DoAttackPattern();
}
