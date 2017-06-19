﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovementClass : MonoBehaviour {

    //components
    private Rigidbody rb;

    //movement fields
    private Vector3 movement_direction;
    [SerializeField]
    private float move_speed;
    [SerializeField]
    private float turn_speed;
    [SerializeField]
    private float jump_speed;

    public bool on_ground;

    private void Awake()
    {
        //initialize components
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //jump
        if (Input.GetButtonDown("Jump") && on_ground)
            rb.AddForce(transform.up * jump_speed);

        //create movement vector
        movement_direction = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);

        //rotate character towards movement direction
        if (movement_direction.magnitude != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movement_direction.x, 0f, movement_direction.z), Vector3.up), turn_speed * Time.deltaTime);

        Vector3 velocity = movement_direction * move_speed * Time.deltaTime;
        //print(velocity);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
            on_ground = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
            on_ground = false;
    }
}
