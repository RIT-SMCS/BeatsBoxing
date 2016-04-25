using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTable : MonoBehaviour {

    private List<string> enemyTypes;

    public List<string> EnemyTypes
    {
        get { return enemyTypes; }
		set { enemyTypes = value; }
    }

	// Use this for initialization
	void Start () {
        enemyTypes = new List<string>();
        Debug.Log(enemyTypes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Add(string type)
    {
        enemyTypes.Add(type);
    }
    public void Remove(int i)
    {
        enemyTypes.RemoveAt(i);
    }
    public void Clear()
    {
        enemyTypes.Clear();
    }
    public GameObject CreateRandom()
    {
        GameObject temp = Instantiate(Resources.Load(enemyTypes[Random.Range(0, enemyTypes.Count)])) as GameObject;
        return temp;
    }    
}
