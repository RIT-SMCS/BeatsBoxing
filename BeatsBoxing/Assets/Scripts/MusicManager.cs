using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	public AudioClip startMusic; //the initial track
	public AudioClip musicPhase2; 
	public AudioClip musicPhase3; 
	public AudioSource source; //the audioSource
	float musicVol; //the music volume
	
	private float scoreBarrier1; //score barriers are for increasing the music
	private float scoreBarrier2; 
	
	private bool switch1; 
	private bool switch2; 
	
	// Use this for initialization
	void Start () {
		
		musicVol = 3.0f; 
		source = GetComponent<AudioSource> (); 
		source.clip = startMusic; 
		//source.PlayOneShot (startMusic, musicVol); 
		source.Play (); 
		scoreBarrier1 = 100.0f; 
		scoreBarrier2 = 200.0f; 
		switch1 = true; 
		switch2 = true; 
	}
	
	// Update is called once per frame
	void Update () {
		if (ScoreManager.Score > scoreBarrier2 && switch2) 
		{
			source.Pause();
			int time = source.timeSamples; 
			//source.PlayOneShot(musicPhase2, musicVol);
			source.clip = musicPhase3; 
			source.timeSamples = time; 
			source.Play(); 
			//source.clip = musicPhase2; 
			//source.Play(); 
			
			switch2 = false; 
		}
		else if(ScoreManager.Score > scoreBarrier1 && switch1)
		{
			source.Pause();
			int time = source.timeSamples; 
			//source.PlayOneShot(musicPhase2, musicVol);
			source.clip = musicPhase2; 
			source.timeSamples = time; 
			source.Play(); 
			//source.clip = musicPhase2; 
			//source.Play(); 
			
			switch1 = false; 
		}
	}
}
