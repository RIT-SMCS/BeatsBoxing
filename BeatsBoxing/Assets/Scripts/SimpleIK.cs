using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SimpleIK : MonoBehaviour {
	//number of iterations to solve IK
	public int iterations = 5;

    static int temp = 0;

	//slows the rotation per iteration
	[Range(0.01f, 1)]
	public float dampening = 1;

	//Location to try to place the endTransform at
	public Transform target;

	//The transform that represents the end of the final limb
	public Transform endTransform;

	//The limits of rotation for the intermediate limbs
	public Node[] angleLimits = new Node[0];

    //The limits of rotation for facing right and left
    public Node[] angleLimitsFacingRight;
    public Node[] angleLimitsFacingLeft;

    //stores the facing-rightness of the 
    bool isFacingRight = true;

	//Dictionary for quick access later
	Dictionary<Transform, Node> nodes;

    Dictionary<Transform, Node> nodesRight;
    Dictionary<Transform, Node> nodesLeft;

	//Serializable so that it's usable before hitting play. Stores the angle limits for a given tranform
	[System.Serializable]
	public class Node{
		public Transform transform;
		public float min;
		public float max;

        public Node Clone()
        {
            Node n = new Node();
            n.min = min;
            n.max = max;
            n.transform = transform;
            return n;
        }
	}

	//Does stuff
	void OnValidate(){
		foreach (Node n in angleLimits) {
			n.min = ClampAngle (n.min, -360, 360);
			n.max = ClampAngle (n.max, -360, 360);
		}
	}

    // Set up the quick-access dictionary
    void Start()
    {
		nodes = new Dictionary<Transform, Node> (angleLimits.Length);
		foreach (Node n in angleLimits) {
			nodes[n.transform] = n;
		}

        angleLimitsFacingLeft = new Node[angleLimits.Length];
        angleLimitsFacingRight = new Node[angleLimits.Length];

        for (int i = 0; i < angleLimits.Length; ++i)
        {
            angleLimitsFacingRight[i] = angleLimits[i].Clone();
            angleLimitsFacingLeft[i] = angleLimits[i].Clone();
        }

        InvertLimits(angleLimitsFacingLeft);

        nodesRight = nodes;
        nodesLeft = new Dictionary<Transform, Node>(angleLimits.Length);
        foreach (Node n in angleLimitsFacingLeft)
        {
            nodesLeft[n.transform] = n;
        }

        SetFacingRight(isFacingRight);
	}
	//run IK
	void LateUpdate () 
	{
		if (!Application.isPlaying) {
			Start ();
		}
		
		if (target == null || endTransform == null) {
			return;
		}
		
		for (int i = 0; i < iterations; ++i) {
			CalculateCCD ();
		}
	}
	//Calculate Inverse Kinematics by Cyclic Coordinate Descent.  
	void CalculateCCD(){
		Transform n = endTransform.parent;
		
		while (true) {
			RotateTowardsTarget(n);

			if (n == gameObject.transform){
				break;
			}
			n = n.parent;
		}
	}
	//Rotates the given transform towards the target
	void RotateTowardsTarget( Transform t ){
		Vector2 toTarget = target.position - t.position;
		Vector2 toEnd = endTransform.position - t.position;
		
		float angle = SignedAngle(toEnd, toTarget);
		
		angle *= dampening;
		
		angle = t.eulerAngles.z - angle;

		if (nodes.ContainsKey (t)) {
			Node n = nodes[t];
			float parentRotation = t.parent ? t.parent.eulerAngles.z : 0;
			angle -= parentRotation;
			angle = ClampAngle (angle, n.min, n.max);
			angle += parentRotation;

		}
		//ClampAngle (angle);

		t.rotation = Quaternion.Euler (0, 0, angle);
	}
	
	public static float SignedAngle(Vector3 a, Vector3 b){
		float angle = Vector3.Angle(a, b);
		//float sign = Mathf.Sign (Vector3.Dot(Vector3.back,Vector3.Cross (a, b)));
		float sign = Mathf.Sign (Vector3.Dot(Vector3.back,Vector3.Cross (a, b)));
		return angle * sign;
	}
	
	float ClampAngle(float angle, float min, float max){
		angle = Mathf.Abs((angle % 360) + 360) % 360;
		//angle = angle % 360;
		angle = Mathf.Clamp(angle, min, max);
		return angle;
	}
    //Invert the angle limits for facing the other direction. 
    void InvertLimits(Node[] limits)
    {
        foreach (var limit in limits)
        {
            limit.min = 359 - limit.min;
            limit.max = 359 - limit.max;
            SwapMinMax(limit);
        }
    }

    void SwapMinMax(Node n)
    {
        float t = n.min;
        n.min = n.max;
        n.max = t;
    }

    public void SetFacingRight(bool isFacingRight)
    {
        if (this.isFacingRight != isFacingRight)
        {
            angleLimits = (isFacingRight) ? angleLimitsFacingRight : angleLimitsFacingLeft;
            nodes = (isFacingRight) ? nodesRight : nodesLeft;
            this.isFacingRight = isFacingRight;
            if (angleLimits.Length > 0)
            {
                Debug.Log(temp++ + ":"+angleLimits[0].min+","+angleLimits[0].max);
            }
        }
    }
}/**/
