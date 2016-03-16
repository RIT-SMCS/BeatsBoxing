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
        if(eManager.Enemies.Count < eManager.maxEnemies)
        {
            eManager.MakeEnemy((int)Random.Range(0.0f,5.0f));
            Debug.Log(eManager.Enemies.Count + " in game.");            
        }
    }
}
