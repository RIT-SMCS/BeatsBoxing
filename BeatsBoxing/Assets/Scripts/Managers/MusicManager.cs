using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip startMusic; //the initial track
	public AudioSource source; //the audioSource
	float musicVol; //the music volume

	// Use this for initialization
	void Start () {

		musicVol = 3.0f; 
		source = GetComponent<AudioSource> (); 
		source.PlayOneShot (startMusic, musicVol); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
