using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandClass : MonoBehaviour {

    public static CharacterCommandClass instance;

    public GameObject shadow_fab;
    public bool shadow_active;

    private void Awake()
    {
        //initialize singleton behavior
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButton(0) && !shadow_active)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 13.0f; //distance of the plane from the camera
            Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);
            GameObject temp = Instantiate(new GameObject(), pos, Quaternion.identity);

            Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets = new Transform[] { this.transform, temp.transform };
        }
        if (Input.GetMouseButtonUp(0) && !shadow_active)
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 13.0f; //distance of the plane from the camera
            Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);

            GameObject shadow = Instantiate(shadow_fab, pos, Quaternion.identity);
            print(-10 * (Mathf.Log(Mathf.Abs(Vector3.Distance(this.transform.position, pos)))) + 40);
            shadow.GetComponent<ShadowInformationClass>().life_time = (-8 * (Mathf.Log(Mathf.Abs(Vector3.Distance(this.transform.position, pos)))) + 25);

            this.gameObject.GetComponent<CharacterLightClass>().currentLightLife -= shadow.GetComponent<ShadowInformationClass>().life_time;

            shadow_active = true;
            Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets = new Transform[] { this.transform, shadow.transform };
        }
        //if (Input.GetMouseButton(0) && !shadow_active)
        //{
        //    Vector3 screenPoint = Input.mousePosition;
        //    screenPoint.z = 13.0f; //distance of the plane from the camera
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);
        //    print(pos);
        //    GameObject shadow = Instantiate(shadow_fab, pos, Quaternion.identity);
        //    print(-10 * (Mathf.Log(Mathf.Abs(Vector3.Distance(this.transform.position, pos)))) + 40);
        //    shadow.GetComponent<ShadowInformationClass>().life_time = (-8 * (Mathf.Log(Mathf.Abs(Vector3.Distance(this.transform.position, pos)))) + 25);
        //    this.gameObject.GetComponent<CharacterLightClass>().currentLightLife -= shadow.GetComponent<ShadowInformationClass>().life_time;
        //    shadow_active = true;
        //    Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets = new Transform[] { this.transform, shadow.transform };
        //}
        if (Input.GetMouseButtonDown(1) && shadow_active)
        {
            Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets = new Transform[] { Camera.main.transform.parent.gameObject.GetComponent<CameraControlScript>().m_Targets[0] };
            Destroy(GameObject.Find("Shadow1(Clone)"));
            shadow_active = false;
        }

    }
}
