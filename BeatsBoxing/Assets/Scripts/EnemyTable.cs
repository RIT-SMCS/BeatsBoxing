using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTable : MonoBehaviour {

    private Dictionary<string, float> enemyTypes;    

    public Dictionary<string, float> EnemyTypes
    {
        get { return enemyTypes; }
		set { enemyTypes = value; }
    }

	// Use this for initialization
	void Start () {
        enemyTypes = new Dictionary<string, float>();
        Debug.Log(enemyTypes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetWeight(string enemy, float weight)
    {
        enemyTypes[enemy] = weight;
    }
    public void Add(string type, float weight)
    {
        enemyTypes.Add(type, weight);
    }
    public void Remove(string enemy)
    {
        enemyTypes.Remove(enemy);
    }
    public void Clear()
    {
        enemyTypes.Clear();
    }
    public GameObject CreateRandom()
    {
        string enemy;
        float totalWeight = 0;
        
        foreach(KeyValuePair<string, float> e in enemyTypes)
        {
            totalWeight += e.Value;
        }
         
        float randomWeight = Random.Range(0,totalWeight);

        //GameObject temp = Instantiate(Resources.Load(enemyTypes[Random.Range(0, enemyTypes.Count)])) as GameObject;
        GameObject temp = new GameObject();
        return temp;
    }    
}
