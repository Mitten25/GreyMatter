using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementClass : MonoBehaviour {

    private Transform player;
    private Vector3 movement_direction;
    private Rigidbody rb;
    public float attack_move_speed;
    public float stalk_move_speed;
    public float move_speed;
    public float turn_speed;

    public enum state { DORMANT, STALKING, ATTACKING };

    public state current_state;

    public Vector3 return_position;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        return_position = this.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        //state
        if (CharacterLightClass.instance.lightOn == false && CharacterLightClass.instance.inLight == false)
        {
            current_state = state.ATTACKING;
            move_speed = attack_move_speed;
        }
        else if (CharacterLightClass.instance.lightOn == false && CharacterLightClass.instance.inLight == true)
        {
            current_state = state.DORMANT;
        }
        else if (CharacterLightClass.instance.lightOn == true && CharacterLightClass.instance.inLight == false)
        {
            current_state = state.STALKING;
            move_speed = stalk_move_speed;
        }

        if (current_state == state.ATTACKING || current_state == state.STALKING)
        {
            //jump
            //if (Input.GetButtonDown("Jump") && on_ground)
            //    rb.AddForce(transform.up * jump_speed);

            //create movement vector
            movement_direction = player.position - this.transform.position;
            movement_direction = movement_direction.normalized;

            //rotate character towards movement direction
            if (movement_direction.magnitude != 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movement_direction.x, 0f, movement_direction.z), Vector3.up), turn_speed * Time.deltaTime);

            Vector3 velocity = movement_direction * move_speed * Time.deltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

            //for 2.5 sake
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        if (current_state == state.DORMANT)
        {
            move_speed = stalk_move_speed;

            movement_direction = return_position - this.transform.position;
            movement_direction = movement_direction.normalized;

            //rotate character towards movement direction
            if (movement_direction.magnitude != 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(movement_direction.x, 0f, movement_direction.z), Vector3.up), turn_speed * Time.deltaTime);

            Vector3 velocity = movement_direction * move_speed * Time.deltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

            //for 2.5 sake
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            print("dead");
        }
    }
}
