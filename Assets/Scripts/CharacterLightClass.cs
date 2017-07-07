using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLightClass : MonoBehaviour
{

    public static CharacterLightClass instance;

    public GameObject pointLight;

    public float maxLightLife = 100f;
    public float currentLightLife = 100f;

    public bool lightOn;
    public bool inLight;

    private Text lightText;

    private void Awake()
    {
        //initialize singleton behavior
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        lightText = GameObject.Find("LightLife").GetComponent<Text>();
        lightOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLightLife <= 0)
        {
            currentLightLife = 0;
            lightOn = false;
            pointLight.GetComponent<Light>().enabled = false;
        }

        if (lightOn)
        {
            currentLightLife -= .1f;
        }

        lightText.text = "Light Remaining: " + currentLightLife;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Light")
        {
            pointLight.GetComponent<Light>().enabled = false;
            lightOn = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Light")
        {
            pointLight.GetComponent<Light>().enabled = true;
            lightOn = true;
            inLight = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Light")
        {
            if (currentLightLife < maxLightLife)
                currentLightLife += .1f;
            inLight = true;
        }
    }
}
