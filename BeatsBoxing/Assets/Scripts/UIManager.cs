using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject gmObj;
    public GameObject ComboMeter, Health;
    Text ComboText, HealthText;
    Dictionary<GameObject, Text> textDict;
    
    // Use this for initialization
    void Start()
    {
        if (gmObj != null)
        {
            gameManager = gmObj.GetComponent<GameManager>();
        }
        ComboText = ComboMeter.GetComponent<Text>();
        HealthText = Health.GetComponent<Text>();

        textDict = new Dictionary<GameObject, Text>();
        textDict.Add(Health, HealthText);
        textDict.Add(ComboMeter, ComboText);
       
    }

    public void SetText(GameObject obj, string newText)
    {
        textDict[obj].text = newText;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {
            ManageTouches();

        }
    }

    void ManageTouches()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;
            float touchDeltaTime = touch.deltaTime;
            Debug.Log("deltaPos: " + touchDeltaPosition);
            Debug.Log("dt: " + touchDeltaTime);
            Debug.Log("deltaPos / dt: " + touchDeltaPosition.magnitude / touchDeltaTime);
            if (touchDeltaPosition.magnitude / touchDeltaTime > 0.5 && touchDeltaPosition.y > 0)
            {
                Debug.Log("SWIPER SWIPED SWIPE SWIPE SWIPE");
            }

        }
    }
}
