using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    public int maxEnemies;
    public GameObject enemyToSpawn;
    List<GameObject> enemies;
    public GameObject player;    

    public List<GameObject> Enemies
    {
        get { return enemies; }
    }

	// Use this for initialization
	void Start() {
        enemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        List<GameObject> toRemove = new List<GameObject>();
	    foreach(GameObject e in enemies)
        {            
            if(e.transform.position.x <= -3.0f)
            {
                toRemove.Add(e);                
            }
        }        
        foreach (GameObject e in toRemove)
        {
            RemoveEnemy(e);            
        }

        foreach (GameObject e in enemies)
        {
            e.GetComponent<Enemy>().XVelocity = (-1.0f - (ScoreManager.SpeedScale - 1.0f));
        }
    }

    //Instantiate an Enemy and add it to list
    public void MakeEnemy(int laneNum)
    {
        GameObject temp = Instantiate(enemyToSpawn);
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
