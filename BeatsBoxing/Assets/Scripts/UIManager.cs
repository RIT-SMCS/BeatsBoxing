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
    GameObject pausePrefab;
    GameObject pauseMenu;

    bool paused = false;

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

        pausePrefab = Resources.Load("PauseMenu") as GameObject;

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
        string curhealth = "";
        for(int i = 0; i < 5; i++)
        {
            if (gameManager._player.Health > i)
                curhealth += " ";
            else curhealth += " ";
        }
        HealthText.text = curhealth;
        ScoreText.text = "Score: " + ScoreManager.Score;
        ComboText.text = "Combo x" + ScoreManager.Combo;
        MultText.text =  "Multiplier x" + ScoreManager.Multiplier;

		SetText (Mobile,"" + Camera.main.GetComponent<Camera> ().WorldToScreenPoint (gameManager._player.startingPos));

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
			} else if (currentTouch.position.x <= Screen.width / 2) {
				if (currentTouch.phase == TouchPhase.Moved) {
					touchDelta = currentTouch.deltaPosition;
					touchTime += currentTouch.deltaTime;
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

    public void Pause()
    {
        
        if (pausePrefab != null && !paused)
        {
            Time.timeScale = 0.0f;
            GameObject pauseMenu = Instantiate(pausePrefab) as GameObject;
            pauseMenu.transform.SetParent(transform);
            pauseMenu.transform.localPosition = new Vector3(0, 0, 0);
            pauseMenu.transform.localScale = new Vector3(1, 1, 1);
            pauseMenu.name = "PauseMenu";
            Button closeBtn = pauseMenu.transform.FindChild("RowFive").transform.FindChild("ResumeButton").GetComponent<Button>() as Button;
            closeBtn.onClick.AddListener(delegate() { Resume(); });
            paused = true;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        Destroy(transform.FindChild("PauseMenu").gameObject);
        pauseMenu = null;
        paused = false;
    }
}
