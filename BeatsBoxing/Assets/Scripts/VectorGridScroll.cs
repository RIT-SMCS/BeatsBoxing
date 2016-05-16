using UnityEngine;
using System.Collections;

public class VectorGridScroll : MonoBehaviour {
    public float ScrollFactor;
    [SerializeField] GameObject topPlane;
    [SerializeField] GameObject botPlane;

    public float duration = 2.0f;
    private Color currentColor, nextColor;

    Color[] colors = new[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };

    System.Random randy = new System.Random();
    Material topMat, botMat;
    private float lerpTime = 0.0f;

    // Use this for initialization
    void Start () {

        topMat = topPlane.GetComponent<Renderer>().material;
        botMat = botPlane.GetComponent<Renderer>().material;
        currentColor = topMat.color;

        int index = randy.Next(0, colors.Length - 1);

        if(colors[index] == currentColor)
        {
            index = (index + 1) % (colors.Length - 1);
        }


        nextColor = colors[index];
    }
	
	// Update is called once per frame
	void Update () {
        lerpTime += Time.deltaTime;

        if (lerpTime / duration > 1.0f)
        {
            lerpTime = 0.0f;
            currentColor = nextColor;

            int index = randy.Next(0, colors.Length - 1);

            if (colors[index] == currentColor)
            {
                index = (index + 1) % (colors.Length - 1);
            }

            

            nextColor = colors[index];

        }


        topMat.color = Color.Lerp(currentColor, nextColor, lerpTime / duration);
        topMat.SetColor("_EmissionColor", topMat.color);
        botMat.color = Color.Lerp(currentColor, nextColor, lerpTime / duration);
        botMat.SetColor("_EmissionColor", botMat.color);

        topMat.mainTextureOffset = new Vector2((topMat.mainTextureOffset.x - (ScrollFactor * ScoreManager.SpeedScale * Time.deltaTime)) % 1.0f, (topMat.mainTextureOffset.y - 0.01f) % 1.0f);
        botMat.mainTextureOffset = new Vector2((botMat.mainTextureOffset.x - (ScrollFactor * ScoreManager.SpeedScale * Time.deltaTime)) % 1.0f, (botMat.mainTextureOffset.y + 0.01f) % 1.0f);
    }
}
