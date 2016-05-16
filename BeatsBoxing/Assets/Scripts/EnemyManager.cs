using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EnemyManager {

    public static int maxEnemies = 10;
    public static int numEnemies = 10;    
    static List<GameObject> enemies = new List<GameObject>(); 
    //public static GameObject player;    
    private static Enemy lastEnemy;
    static float minX;

    static int enemyID = 0;    

    [SerializeField]
    private static float beatsPerMinute = 150;

    public static List<GameObject> Enemies
    {
        get { return enemies; }
    }    
	
	// Update is called once per frame
	public static void Update () {
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
    public static void MakeEnemy(int laneNum)
    {
		if (enemies.Count < maxEnemies) {
            GameObject temp;

            if (lastEnemy == null) { temp = GameObject.Instantiate(Resources.Load("BasicEnemyPrefab")) as GameObject; }
            else { temp = lastEnemy.ETable.CreateRandom(); }            

			Enemy en = temp.GetComponent<Enemy>();
			//set the XVelocity to the appropriate speed
			en.XVelocity = (-1.0f - (ScoreManager.SpeedScale - 1.0f));
            //get the current x position of the Player's Attack hitbox
            float attackPositionX = GameObject.FindObjectOfType<Player>().gameObject.GetComponentInChildren<Attack>().transform.position.x;
			//find the change in x position per beat of the song
			float dxPerBeat = 60.0f * -en.XVelocity / (beatsPerMinute);
			//find how many beats it will take to ensure the enemy spawns off the right side of the screen
			int beatsToTravel = 1 + (int)((Camera.main.ScreenToWorldPoint(Screen.width * Vector3.right).x - attackPositionX) / dxPerBeat);
			//find the distance between the spawn and the attack hitbox
			float deltaX = dxPerBeat * beatsToTravel;
			//find the time it will take the enemy to reach the hitbox
			float timeToTravel = deltaX / -en.XVelocity;
            //set the bubble duration to disappear the moment it reaches the hitbox
            //en.BubbleDuration = timeToTravel;
            en.StartX = attackPositionX + deltaX;            
            temp.transform.position = new Vector3(en.StartX, 0.0f, 0.0f);
			en.Lane = laneNum;
			en.ReadyUp();
			temp.transform.parent = temp.transform; //this.transform?
			en.name = "enemy " + (++enemyID);
			enemies.Add(temp);
            lastEnemy = en;
		}
    }
    
    //remove Enemy from List and destroy it
    public static void RemoveEnemy(GameObject e)
    {
        enemies.Remove(e);
        Object.Destroy(e);
    }
    public static void Reset()
    {
        lastEnemy = null;
        minX = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(Vector3.zero).x;
        beatsPerMinute = 150;
        enemyID = 0;
        enemies.Clear();
    }
}
