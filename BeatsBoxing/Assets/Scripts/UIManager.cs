using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject gmObj;
    [SerializeField] GameObject ComboMeter, Health, Mobile, Mult, Score;
    Text ComboText, HealthText, MobileText, MultText, ScoreText;
    Dictionary<GameObject, Text> textDict;
    
	float touchTime;
	Vector2 touchDelta;


    // Use this for initialization
    void Start()
    {
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
        ComboText = ComboMeter.GetComponent<Text>();
        HealthText = Health.GetComponent<Text>();
		MobileText = Mobile.GetComponent<Text> ();
        MultText = Mult.GetComponent<Text>();
        ScoreText = Score.GetComponent<Text>();

        textDict = new Dictionary<GameObject, Text>();
        textDict.Add(Health, HealthText);
        textDict.Add(ComboMeter, ComboText);
		textDict.Add(Mobile, MobileText);
       
    }

    public void SetText(GameObject obj, string newText)
    {
        textDict[obj].text = newText;
    }



    // Update is called once per frame
    void Update()
    {

        ManageTouches();
        HealthText.text = "HP: "+ gameManager._player.Health;
        ScoreText.text = "Score: " + ScoreManager.Score;
        ComboText.text = "Combo x" + ScoreManager.Combo;
        MultText.text =  "Multiplier x" + ScoreManager.Multiplier;

    }

    void ManageTouches()
	{

		if (Input.touchCount > 0) {
			Touch currentTouch = Input.GetTouch (0);
			if (currentTouch.phase == TouchPhase.Began) {
				touchDelta = new Vector2 (0.0f, 0.0f);
				touchTime = 0.0f;
				if (currentTouch.position.x > Screen.width / 2) {
					gameManager.Attack ();
				}
			} else if (currentTouch.phase == TouchPhase.Moved) {
				touchDelta = currentTouch.deltaPosition;
				touchTime += currentTouch.deltaTime;
				SetText (Mobile, "dt: " + touchTime + "\ndeltaPos / dt: " + touchDelta.magnitude / touchTime);

			} else if (currentTouch.phase == TouchPhase.Ended) {
				if (touchDelta.magnitude / touchTime > 0.5) {
					if (touchDelta.y > 0) {
						gameManager._player.Lane++;
					} else if (touchDelta.y < 0) {
						gameManager._player.Lane--;	
					}

				}
			}
		}
	}
}
