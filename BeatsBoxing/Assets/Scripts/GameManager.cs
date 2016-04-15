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
    public float spawnThreshold;
    public float timeToSpawn;


    [SerializeField] private float startDelay;
    [SerializeField] private float spawnRate;

	// Use this for initialization
	void Awake () {
		//InvokeRepeating("SpawnEnemies", startDelay, spawnRate);
		
        for (int i = 0; i < LaneActor.MAX_LANES+1; ++i)
        {
            GameObject lb = Instantiate(LaneBarrierPrefab, new Vector3(0.0f, 1.0f * (-3 + i) - 0.5f, 1.0f), Quaternion.identity) as GameObject;
            if (i != 0 && i != LaneActor.MAX_LANES)
            {
                lb.transform.localScale = new Vector3(lb.transform.localScale.x, 0.5f * lb.transform.localScale.y, lb.transform.localScale.z);
            }
            lb.transform.parent = this.transform;
            lb.name = "barrier " + i;
            //Lane l = Instantiate(LanePrefab, new Vector3(-Camera.main.orthographicSize, 1.0f * (-3 + i), 0.0f), Quaternion.identity) as Lane;
            //l.transform.parent = this.transform;
            //l.name = "lane " + i;
            //for (int j = 0; j < 100; ++j)
            //{
            //    Lane subLane = Instantiate(LanePrefab, new Vector3(l.transform.position.x + 0.3f*(j+1), 1.0f * l.transform.position.y, 0.0f), Quaternion.identity) as Lane;
            //    subLane.transform.parent = l.transform;
            //    subLane.name = ""+j;
            //    subLane.IsParent = false;
            //}
        }

    }
	
	// Update is called once per frame
	void Update () {
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
        if(timeToSpawn >= spawnThreshold)
        {
            SpawnEnemies();
            timeToSpawn = 0.0f;
        }
        timeToSpawn += Time.deltaTime * ScoreManager.SpeedScale;
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
