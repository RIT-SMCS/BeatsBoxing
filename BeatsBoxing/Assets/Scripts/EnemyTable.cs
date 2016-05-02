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
        float randWeighted = RandomWeighted();

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            
        }

        //GameObject temp = Instantiate(Resources.Load(enemyTypes[Random.Range(0, enemyTypes.Count)])) as GameObject;
        GameObject temp = new GameObject();
        return temp;
    }
    //Obtained from http://forum.unity3d.com/threads/selection-based-on-percentage-weighting-in-c.274680/
    public float RandomWeighted()
    {
        float totalWeight = 0.0f, result = 0.0f, total = 0.0f;

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            totalWeight += e.Value;
        }

        float rand = Random.Range(0.0f, totalWeight + 1.0f);

        foreach (KeyValuePair<string, float> e in enemyTypes)
        {
            result += 0.1f;
            total += e.Value;
            if (total > rand) break;
        }
        return result;
    }
}
