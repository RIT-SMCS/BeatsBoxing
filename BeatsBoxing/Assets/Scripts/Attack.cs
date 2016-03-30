using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

	public bool attacking; 

	// Use this for initialization
	void Start () {
		attacking = false; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (attacking && other.gameObject.CompareTag("Enemy")) 
		{
			//deal damage if attacking a valid enemy
			//other.gameObject.GetComponent<LaneActor>().Health = other.gameObject.GetComponent<LaneActor>().Health - 2; 
			other.gameObject.GetComponent<Enemy>().TakeDamage(1); 
			//end the attack after hitting something
			attacking = false; 

			Debug.Log("ENEMY HIT"); 
		}
	}
}
