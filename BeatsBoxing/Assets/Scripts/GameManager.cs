using UnityEngine;
using System.Collections;


//static float[] LanePositions = new float[] {   };

public class GameManager : MonoBehaviour
{

    public Player player;
    public EnemyManager eManager; 

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2.0f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.Lane++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.Lane--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemies();
        }        
    }

    void SpawnEnemies()
    {
        eManager.MakeEnemy((int)Mathf.Floor(Random.Range(0.0f,6.0f)));
        Debug.Log(eManager.Enemies.Count + " in game.");       
    }


	public void Attack() {
		//SOMEBODY: Can't call DoAttackPattern!
		//player.DoAttackPattern ();
	}
}
