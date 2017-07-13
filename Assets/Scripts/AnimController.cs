using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

	private Animator anim;
    public string animString;

	// Use this for initialization
	void Start () {

		anim = this.transform.parent.gameObject.GetComponent<Animator>();
        anim.Play(animString);
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown("1"))
		//{
		//	anim.Play ("doorOpen");
		//}
	}
}
