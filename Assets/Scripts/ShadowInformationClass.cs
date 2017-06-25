using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowInformationClass : MonoBehaviour {

    public static ShadowInformationClass instance;

    public float life_time;
    public Text life_time_text;


    private void Awake()
    {
        //initialize singleton behavior
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        GameObject.Find("ShadowTime").GetComponent<Text>().enabled = true;
        life_time_text = GameObject.Find("ShadowTime").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        life_time -= Time.deltaTime;
        if (life_time < 0)
        {
            CharacterCommandClass.instance.shadow_active = false;
            Destroy(this.gameObject);
            GameObject.Find("ShadowTime").GetComponent<Text>().enabled = false;
            Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets = new Transform[] { Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets[0] };
        }
        life_time_text.text = "Shadow Time Remaining : " + life_time;
	}
}
