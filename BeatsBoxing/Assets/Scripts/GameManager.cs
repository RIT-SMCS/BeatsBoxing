using UnityEngine;
using System.Collections;


//static float[] LanePositions = new float[] {   };
//[RequireComponent (typeof(ScoreManager))]
public class GameManager : MonoBehaviour {

    public Lane LanePrefab;
    public Player _player;
	public EnemyManager eManager; 

	// Use this for initialization
	void Awake () {
		InvokeRepeating("SpawnEnemies", 2.0f, 2.0f);
		
        for (int i = 0; i < LaneActor.MAX_LANES; ++i)
        {
            Lane l = Instantiate(LanePrefab, new Vector3(0.0f, 1.0f * (-3 + i), 0.0f), Quaternion.identity) as Lane;
            l.transform.parent = this.transform;
            l.name = "lane " + i;
        }

        ScoreManager.Combo = 0;
        ScoreManager.Score += 10;
        //ScoreManager.Combo = 50;
        ScoreManager.Score += 10;
        ScoreManager.Combo = 100;
        ScoreManager.Score += 10;
        

        Debug.Log(ScoreManager.Score);
        ScoreManager.Score = 100;
        Debug.Log(ScoreManager.Score);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _player.Lane++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _player.Lane--;
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
