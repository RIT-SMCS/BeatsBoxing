using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Bullet : MonoBehaviour {

    private float velocity; 
    private float minX;
    private bool isReflected;

    public float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    public bool IsReflected
    {
        get { return isReflected; }
        set { isReflected = value; }
    }

	// Use this for initialization
	void Start () {
        isReflected = false;
        velocity = -2.0f - (ScoreManager.SpeedScale - 1.0f);
    }

    void Awake()
    {
        minX = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero).x;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(velocity, 0.0f, 0.0f) * Time.deltaTime;

        if(this.transform.position.x <= minX)
        {
            Destroy(this.gameObject);
        }       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().TakeDamage(1);
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Enemy" && isReflected == true)
        {
            col.gameObject.GetComponent<Enemy>().Health -= 1;
            Destroy(this.gameObject);
        }
    }
}
