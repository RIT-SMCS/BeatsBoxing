using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class HighScoreManager : MonoBehaviour {

	private int score1; 
	private int score2;
	private int score3;

	// Use this for initialization
	void Start () {
		//load in stored scores
		if (PlayerPrefs.HasKey ("score1"))
			score1 = (int)PlayerPrefs.GetInt ("score1");
		else
			score1 = 0; 
		if (PlayerPrefs.HasKey ("score2"))
			score2 = (int)PlayerPrefs.GetInt ("score2");
		else
			score2 = 0; 
		if (PlayerPrefs.HasKey ("score3"))
			score3 = (int)PlayerPrefs.GetInt ("score3");
		else
			score3 = 0; 

		//get the player's score
		int playerScore = (int)ScoreManager.Score; 

		//test to see if it's high score material and set the scores
		if (playerScore > score1) 
		{
			score3 = score2; 
			score2 = score1; 
			score1 = playerScore; 
		} 
		else if (playerScore > score2) 
		{
			score3 = score2; 
			score2 = playerScore; 
		} 
		else if (playerScore > score3) 
		{
			score3 = playerScore; 
		}

		//re-store the scores in playerprefs
		PlayerPrefs.SetInt ("score1", score1); 
		PlayerPrefs.SetInt ("score2", score2); 
		PlayerPrefs.SetInt ("score3", score3); 

		this.transform.Find ("Score1").gameObject.GetComponent<Text>().text = "1. " + score1; 
		this.transform.Find ("Score2").gameObject.GetComponent<Text>().text = "2. " + score2; 
		this.transform.Find ("Score3").gameObject.GetComponent<Text>().text = "3. " + score3; 
		Debug.Log ("1. " + score1); 
		Debug.Log ("2. " + score2); 
		Debug.Log ("3. " + score3); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
