using UnityEngine;
using System.Collections;

public class TelegraphAttackBubble : MonoBehaviour {
    public Color startColor = Color.gray;
    public Color endColor = Color.red;
    public Vector3 startScale = new Vector3(10, 5, 1);
    public Vector3 endScale = new Vector3(1, 1, 1);
    SpriteRenderer renderer;
    public float duration = 2.0f;
    public float age;

	// Use this for initialization
	void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = startColor;
        age = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        float t = Mathf.Min(age, duration) / duration;
        renderer.color = Color.Lerp(startColor, endColor, t);
        transform.localScale = Vector3.Lerp(startScale, endScale, t);

        if(t >= 1.0)
        {
            Destroy(gameObject);
        }
	}
}
