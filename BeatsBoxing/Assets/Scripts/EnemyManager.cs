using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

    List<Enemy> enemies;

	// Use this for initialization
	void Start() {
        enemies = new List<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void MakeEnemy(int laneNum)
    {
        Enemy temp = new Enemy();
        temp.Lane = laneNum;
        enemies.Add(temp);
    }
}
