using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements; 

public class AdManager : MonoBehaviour {

	void Start ()
	{
		Advertisement.Initialize ("1068742", true);
		
		StartCoroutine (ShowAdWhenReady ());
	}
	
	IEnumerator ShowAdWhenReady()
	{
		while (!Advertisement.IsReady ())
			yield return null;
		
		Advertisement.Show ();
	}
}
