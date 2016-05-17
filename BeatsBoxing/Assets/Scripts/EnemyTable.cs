using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTable{

    private Dictionary<string, float> enemyTypes = new Dictionary<string, float>();    

    public Dictionary<string, float> EnemyTypes
    {
        get { return enemyTypes; }
		set { enemyTypes = value; }
    }

	// Use this for initialization
	void Start () {        
        
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
        float randWeighted = RandomWeighted();
        float temp = 0.0f;
        KeyValuePair<string, float> toChoose = new KeyValuePair<string, float>();

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            if(e.Value >= randWeighted)
            {
                if(temp == 0.0f || e.Value - randWeighted < temp - randWeighted)
                {
                    temp = e.Value;
                    toChoose = e;
                }          
                if(temp == 0.0f || e.Value - randWeighted == temp - randWeighted)
                {
                    int tempRand = Random.Range(0, 1);
                    if(tempRand == 1)
                    {
                        temp = e.Value;
                        toChoose = e;
                    }
                }
            }
        }

        GameObject enemy;
        if (toChoose.Key != null)
        {
            enemy = GameObject.Instantiate(Resources.Load(toChoose.Key)) as GameObject;
        }
        else
        {
            enemy = GameObject.Instantiate(Resources.Load("BasicEnemyPrefab")) as GameObject;
        }
        
        return enemy;
    }
    //Obtained from http://forum.unity3d.com/threads/selection-based-on-percentage-weighting-in-c.274680/
    public float RandomWeighted()
    {
        float totalWeight = 0.0f, result = 0.0f, total = 0.0f;

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            totalWeight += e.Value;
        }

        float rand = Random.Range(0.0f, totalWeight);

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            result += 1.0f;
            total += e.Value;
            if (total > rand) break;
        }
        //Debug.Log("Total Weight: " + totalWeight);
        //Debug.Log("Random: " + rand);
        //Debug.Log("Result: " + result);
        return result;
    }
}
