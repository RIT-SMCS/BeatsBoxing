using UnityEngine;
using System.Collections;


//static float[] LanePositions = new float[] {   };

public class GameManager : MonoBehaviour {


    public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.Lane++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.Lane--;
        }
    }
}
