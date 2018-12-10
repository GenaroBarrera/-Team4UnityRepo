using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f; //controls how fast the player is (the f at the end of 6 denotes that it's a floating point variable)
    public float defaultSpeed = 6f; //this is our default speed
    //private variales
    Vector3 movement; //use this to store the movement we want to apply to the player
    Animator anim; //anim is a reference to the animatior component
    Rigidbody playerRigidbody; //reference to the rigidbody component
    int floorMask; //(remember that flooor quad) that's the thing we want to raycast into. We use a layer layer mask stored as an integer in order to acheive this.
    float camRayLength = 100; //this will be the length of the ray that we cast from the camera

    void Awake() //similar to the start function, but it gets pulled regardless the script is enabled or not.
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() //unlike regular update. fixedupdate can run once, zero, or several times per frame
    {
        float h = Input.GetAxisRaw("Horizontal"); //note raw axix will only have a value of -1 0 or 1. It won't have any variation inbetween (this will allow our character to instantly get to speed much more responsive) 
        float v = Input.GetAxisRaw("Vertical");

        //now call the functions we've created move, turning, and animating (since they're in fixedupdate they'll be called on every physics step?)
        Move(h, v);
        Turning();
        Animating(h, v);

    }

    void Move(float h, float v) //this is Move function (Animation and Rotation will have their own seperate functions)
    {
        movement.Set(h, 0f, v); //h input for x axis, no vertical y axis input, and v for z axis
        movement = movement.normalized * speed * Time.deltaTime; //this will normalize the speed when  moving using both the x and z axis or strafing in a diagnol. 

        playerRigidbody.MovePosition(transform.position + movement); //apply the movement to the player
    }

    void Turning() // this is our rotation function
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //first create a ray that is cast from the camera into the scene
        RaycastHit floorHit;
        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v) //this is our animating function
    {
        bool walking = h != 0f || v != 0f; //Basically did we press the horizaontal axis, or the vertical axis? if we've pressed either we're walking if not we're not
        anim.SetBool("IsWalking", walking); // set to walking, the variable we;ve just made
    }


    // Use this for initialization
    void Start () { 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
