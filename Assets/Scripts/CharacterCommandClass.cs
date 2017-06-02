using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandClass : MonoBehaviour {

    public GameObject shadow;
	
	// Update is called once per frame
	void Update ()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 13.0f; //distance of the plane from the camera
            Vector3 pos = Camera.main.ScreenToWorldPoint(screenPoint);
            print(pos);
            Instantiate(shadow, pos, Quaternion.identity);
        }
	}
}
