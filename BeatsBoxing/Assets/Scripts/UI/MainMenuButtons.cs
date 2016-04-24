using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

    GameObject instructionsPrefab;
    GameObject instructionsPanel;

    // Use this for initialization
    void Start () {
        instructionsPrefab = Resources.Load("InstructionsPanel") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void OpenMenu()
    {
        if (instructionsPrefab != null && instructionsPanel == null)
        {
            instructionsPanel = Instantiate(instructionsPrefab);
            instructionsPanel.transform.SetParent(this.transform);
            instructionsPanel.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            instructionsPanel.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            instructionsPanel.name = "InstructionsPanel";
            Button menuBtn = instructionsPanel.transform.FindChild("Text").transform.FindChild("CloseButton").GetComponent<Button>() as Button;
            menuBtn.onClick.AddListener(delegate () { CloseMenu(); });
        }
    }

    public void CloseMenu()
    {
        Destroy(this.transform.FindChild("InstructionsPanel").gameObject);
        instructionsPanel = null;
    }
}
