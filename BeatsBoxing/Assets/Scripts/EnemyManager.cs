using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public int maxEnemies;
    public int numEnemies;    
    List<GameObject> enemies;
    public GameObject player;
    private string[] enemyTable;
    float minX;  

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
            e.GetComponent<Enemy>().XVelocity = (-1.0f - (ScoreManager.SpeedScale - 1.0f));
        }
    }

    //Instantiate an Enemy and add it to list
    public void MakeEnemy(int laneNum)
    {
        GameObject temp = Instantiate(Resources.Load(enemyTable[Random.Range(0, numEnemies)])) as GameObject;
        temp.transform.position = new Vector3(temp.GetComponent<Enemy>().StartX, 0.0f, 0.0f);
        temp.GetComponent<Enemy>().Lane = laneNum;
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
