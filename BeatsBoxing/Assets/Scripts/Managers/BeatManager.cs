using UnityEngine;
using System.Collections;

public class BeatManager : MonoBehaviour {
	private static BeatManager s_instance = null;
    //Singleton Instance setter
	public static BeatManager Instance {
		get {
			if (s_instance == null) {
				s_instance = FindObjectOfType(typeof(BeatManager)) as BeatManager;
			}
			if (s_instance == null) {
				GameObject obj = new GameObject("BeatManager");
				s_instance = obj.AddComponent(typeof(BeatManager)) as BeatManager;
				Debug.Log ("No BeatManager found. New one created");
			}
			return s_instance;
		}
	}

	//the delegate definition to control events happening on the beat
	public delegate void OnBeat();
	//the delegate that holds all methods to execute on the beat
	public OnBeat ExecuteOnBeat;

	//Beat timing relevent vars
	public int m_BPM = 60;
	public float m_BPM_Offset = 0.0f;

    private float m_timePerBeat;
	private float m_timeOfNextBeat;

	private bool m_isOnBeat = false;

	//properties
	public int BPM {
		get { return m_BPM; }
		set {
            string str = "";
			m_BPM = Mathf.Max (1, value);
            m_timeOfNextBeat -= m_timePerBeat;
            str += m_timeOfNextBeat + " <> ";
			m_timePerBeat = 60.0f / m_BPM;
            m_timeOfNextBeat += m_timePerBeat;
            Debug.Log(str + m_timeOfNextBeat);
        }
	}
	public float BPM_Offset {
		get { return m_BPM_Offset; }
		set { 
			float beatWithoutOffset = m_timeOfNextBeat - m_BPM_Offset;
			m_BPM_Offset = Mathf.Max (1, value); 
			m_timeOfNextBeat += m_BPM_Offset;
		}

	}

    public bool IsOnBeat
    {
        get { return m_isOnBeat; }
    }

    public float TimePerBeat
    {
        get { return m_timePerBeat; }
    }

	// Use this for initialization
	void Start () {
		m_timeOfNextBeat = m_BPM_Offset + m_timePerBeat;
        OnValidate();
	}

    void OnValidate()
    {
        BPM = m_BPM;
        BPM_Offset = m_BPM_Offset;
    }
	
	// Update is called once per frame
	void Update () {
        //if it is time to fire a beat
		if (Time.time > m_timeOfNextBeat) {
			m_isOnBeat = true;
			m_timeOfNextBeat += m_timePerBeat;
			ExecuteOnBeat();
            Debug.Log("Beat at: " + Time.time);
		} else {
			m_isOnBeat = false;
		}
	}
    //Clear stuff when the app ends
	void OnApplicationQuit() {
		s_instance = null;
	}

}
