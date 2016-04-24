using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public int maxEnemies;
    public int numEnemies;
    public GameObject enemyToSpawn;
    List<GameObject> enemies;
    public GameObject player;
    private string[] enemyTable;
    float minX;


    [SerializeField]
    private float beatsPerMinute;

    public List<GameObject> Enemies
    {
        get { return enemies; }
    }
    public string[] ETable
    {
        get { return enemyTable; }
        set { enemyTable = value; }
    }
	// Use this for initialization
	void Start() {        
        enemies = new List<GameObject>();
        enemyTable = new string[numEnemies];
        enemyTable[0] = "BasicEnemyPrefab";
        enemyTable[1] = "BasicEnemyPrefab";
        enemyTable[2] = "BasicEnemyPrefab";
        enemyTable[3] = "BasicEnemyPrefab";
        enemyTable[4] = "TrackingEnemyPrefab";
        enemyTable[5] = "TrackingEnemyPrefab";
        enemyTable[6] = "TrackingEnemyPrefab";
        enemyTable[7] = "TurretEnemyPrefab";
        enemyTable[8] = "TurretEnemyPrefab";
        enemyTable[9] = "SpikesPrefab";
    }

    void Awake()
    {
        minX = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero).x;
    }
	
	// Update is called once per frame
	void Update () {
        List<GameObject> toRemove = new List<GameObject>();
	    foreach(GameObject e in enemies)
        {            
            if(e.transform.position.x <= minX || e.GetComponent<LaneActor>().Health <= 0)
            {
                toRemove.Add(e);
            }
        }        
        foreach (GameObject e in toRemove)
        {
            RemoveEnemy(e);
            ScoreManager.AddScoreWithMultiplier(10);       
        }

        foreach (GameObject e in enemies)
        {
            //e.GetComponent<Enemy>().XVelocity = (-1.0f - (ScoreManager.SpeedScale - 1.0f));
        }
    }

    //Instantiate an Enemy and add it to list
    public void MakeEnemy(int laneNum)
    {
        GameObject temp = Instantiate(Resources.Load(enemyTable[Random.Range(0, numEnemies)])) as GameObject;
        Enemy en = temp.GetComponent<Enemy>();
        //set the XVelocity to the appropriate speed
        en.XVelocity = (-1.0f - (ScoreManager.SpeedScale - 1.0f));
        //get the current x position of the Player's Attack hitbox
        float attackPositionX = player.GetComponentInChildren<Attack>().transform.position.x;
        //find the change in x position per beat of the song
        float dxPerBeat = 60.0f * -en.XVelocity / (beatsPerMinute);
        //find how many beats it will take to ensure the enemy spawns off the right side of the screen
        int beatsToTravel = 1 + (int)((Camera.main.ScreenToWorldPoint(Screen.width * Vector3.right).x - attackPositionX) / dxPerBeat);
        //find the distance between the spawn and the attack hitbox
        float deltaX = dxPerBeat * beatsToTravel;
        //find the time it will take the enemy to reach the hitbox
        float timeToTravel = deltaX / -en.XVelocity;
        //set the bubble duration to disappear the moment it reaches the hitbox
        en.BubbleDuration = timeToTravel;
        temp.transform.position = new Vector3((en.StartX = attackPositionX + deltaX), 0.0f, 0.0f);
        en.Lane = laneNum;
        temp.transform.parent = this.transform;
        enemies.Add(temp);        
    }
    
    //remove Enemy from List and destroy it
    public void RemoveEnemy(GameObject e)
    {
        enemies.Remove(e);
        Destroy(e);
    }
}
