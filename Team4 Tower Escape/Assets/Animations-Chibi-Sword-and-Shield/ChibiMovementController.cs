using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiMovementController : MonoBehaviour {
    Animator anim;
    Rigidbody rb;
    // Transform trans;
    bool isMoving, isWalking, isDashing;
    int floorMask;
    public float moveSpeed, walkSpeed, dashSpeed, dashDuration;
    Vector3 dashDirection;
    private float dashTime = 0f;
    float camRayLength = 100f;
    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        //trans = gameObject.GetComponent<Transform>();
        floorMask = LayerMask.GetMask("Floor");
        // weapon = gameObject.GetComponent<WeaponStats>();
        // weapon = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponStats>();

        //runSpeed = 0.08f;
        //walkSpeed = 0.03f;
        //dashMultiplier = 1.5f;
        isMoving = false;
        isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputLogicAndMovement();
        WeaponLogic();
        Turning();
        Animating();

    }

    void InputLogicAndMovement()
    {
        //Vector3 localDir = new Vector3(-1f * Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"), 0f,  Input.GetAxis("Vertical") + 1f * Input.GetAxis("Horizontal"));
        Vector3 worldVelo = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        rb.position = new Vector3(rb.position.x, 0.05f, rb.position.z); //ground player
        rb.velocity = worldVelo * walkSpeed; //default update

        //if (Input.GetKeyDown(KeyCode.Space))
        //    //rb.AddForce(localDir.normalized * dashMultiplier, ForceMode.VelocityChange);
        //    trans.position += (localDir.normalized *  dashMultiplier);

        // check for movement input and set isMoving if dash isn't already activated.
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && isDashing == false)
        {
            isMoving = true;

            if (Input.GetKey(KeyCode.LeftShift))
                isWalking = true;
            else
                isWalking = false;

            // dash can only be activated when there is already movement and dash isn't already active.
            // dash also cancels other movement types
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isDashing)
                {
                    isDashing = true;
                    dashTime = dashDuration;
                    dashDirection = worldVelo.normalized;
                    isMoving = false;
                    isWalking = false;
                }
            }
        }
        else
            isMoving = false;

        // manages the time left for dashing and sets isDashing to false once time runs out
        if (dashTime >= 0f)
        {
            dashTime -= Time.deltaTime;
        }
        else
            isDashing = false;

        

        if (isMoving)
        {
            rb.velocity = worldVelo * moveSpeed;
            if (isWalking)
            {
                rb.velocity = worldVelo * walkSpeed;
            }
        }
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);
        }
    }

    void Animating()
    {
        // needed for velocity-based animation
        var localVelocity = transform.InverseTransformDirection(rb.velocity);

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("moveX", localVelocity.x);
        anim.SetFloat("moveZ", localVelocity.z);
        // anim.SetBool("Fire", (Firing));
        //anim.SetBool("AltFire", Input.GetMouseButton(1));
    }

    void WeaponLogic()
    {
        //Firing = false;
        //if (Input.GetMouseButton(0))
        //{
        //    weapon.FireWeapon();
        //    Firing = true;
        //}
        if (Input.GetMouseButtonDown(0))
            anim.SetTrigger("attack");
        anim.SetBool("isBlocking", Input.GetMouseButton(1));
    }
}
