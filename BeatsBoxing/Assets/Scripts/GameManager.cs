using UnityEngine;
using System.Collections;


//static float[] LanePositions = new float[] {   };
//[RequireComponent (typeof(ScoreManager))]
public class GameManager : MonoBehaviour {

    public Lane LanePrefab;
    public GameObject LaneBarrierPrefab;
    public Player _player;
	public EnemyManager eManager;
    public GameObject TelegraphPrefab;


    [SerializeField] private float startDelay;
    [SerializeField] private float spawnRate;

    private float lastSpawnTime;

    // Use this for initialization
    void Awake () {
        lastSpawnTime = startDelay;
		//InvokeRepeating("SpawnEnemies", startDelay, spawnRate * ScoreManager.SpeedScale);
		ScoreManager.Reset();
        

    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= lastSpawnTime + spawnRate / (0.9f * ScoreManager.SpeedScale)) 
        {
            lastSpawnTime = Time.time;
            SpawnEnemies();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemies();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ScoreManager.Combo += 10;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ScoreManager.AddScoreWithMultiplier(10);
        }
    }

    void SpawnEnemies()
    {
        eManager.MakeEnemy((int)Mathf.Floor(Random.Range(0.0f,6.0f)));      
    }


	public void Attack() {
		//SOMEBODY: Can't call DoAttackPattern!
		_player.DoAttackPattern ();
	}
}
