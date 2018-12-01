using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiMovementControllerOld : MonoBehaviour {
 //   Animator anim;
 //   Rigidbody rb;
 //   Transform trans;
 //   bool isMoving, isWalking;
 //   public float runSpeed, walkSpeed, dashMultiplier;
	//// Use this for initialization
	//void Start () {
 //       anim = gameObject.GetComponent<Animator>();
 //       rb = gameObject.GetComponent<Rigidbody>();
 //       trans = gameObject.GetComponent<Transform>();
 //       runSpeed = 0.08f;
 //       walkSpeed = 0.03f;
 //       dashMultiplier = 1.5f;
 //       isMoving = false;
 //       isWalking = false;
	//}
	
	//// Update is called once per frame
	//void FixedUpdate () {
 //       Vector3 localDir = new Vector3(-1f * Input.GetAxis("Vertical") + Input.GetAxis("Horizontal"), 0f,  Input.GetAxis("Vertical") + 1f * Input.GetAxis("Horizontal"));

 //       if (Input.GetKeyDown(KeyCode.Space))
 //           //rb.AddForce(localDir.normalized * dashMultiplier, ForceMode.VelocityChange);
 //           trans.position += (localDir.normalized *  dashMultiplier);
 //       if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
 //           isMoving = true;
 //       else
 //           isMoving = false;
 //       anim.SetBool("isMoving", isMoving);
 //       if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
 //           isWalking = true;
 //       else
 //           isWalking = false;
 //       anim.SetBool("isWalking", isWalking);
 //       if (isMoving)
 //       {
 //           if (isWalking)
 //           {
 //               trans.position += localDir * walkSpeed;//new Vector3(Input.GetAxis("Horizontal") * walkSpeed, 0f, Input.GetAxis("Vertical") * walkSpeed);
 //           }
 //           else
 //           { 
 //               trans.position += localDir * runSpeed;//new Vector3(Input.GetAxis("Horizontal") * runSpeed, 0f, Input.GetAxis("Vertical") * runSpeed);
 //           }
 //       }
        
 //       if (Input.GetMouseButtonDown(0))
 //           anim.SetTrigger("attack");
 //       anim.SetBool("isBlocking", Input.GetMouseButton(1));
 //   }
}
