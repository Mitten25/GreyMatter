using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovementClass : MonoBehaviour {

    public static ShadowMovementClass instance;

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

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
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

        //for 2.5 sake
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
            on_ground = true;
        if (collision.collider.transform.tag == "Light")
            Destroy(this);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.tag == "Ground")
            on_ground = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Light")
        {
            CharacterCommandClass.instance.KillShadow();
        }
        if (other.transform.tag == "Death")
        {
            CharacterCommandClass.instance.KillShadow();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Light")
        {
        }
    }
}
