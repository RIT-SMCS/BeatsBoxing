using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BackgroundPreview))]
public class BackgroundScroll : MonoBehaviour {
    public float ScrollSpeed;

    private float tileSizeX;
    private Vector3 startPosition;

    static int ID = 0;
	// Use this for initialization
	void Awake () {
        startPosition = transform.position;
        BackgroundPreview bp = gameObject.GetComponent<BackgroundPreview>();
        tileSizeX = bp.Width * bp.TileSprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        float newPosition = Mathf.Repeat(Time.time * ScrollSpeed * ScoreManager.SpeedScale, tileSizeX);
        transform.position = startPosition + Vector3.left * newPosition;
	}
}
