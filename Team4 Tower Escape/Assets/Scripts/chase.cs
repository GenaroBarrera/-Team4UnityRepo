using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {
    public Transform player; //exposed variable in our inspector, we can pass through the fps controller through this. it will let the skeleton know the distance between him and the player

    static Animator anim; //this is our animator object

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>(); //set anim to the animator component attached to the skeleton
    }

    // Update is called once per frame
    void Update () {
        Vector3 direction = player.position - this.transform.position; //work out the direction from the player to the skeleton
        float angle = Vector3.Angle(direction, this.transform.forward); //we need to calculate the angle between the the foward direction of the skeleton and the direction towards the player (field of vision)

        if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 30) //using this if, check the distance between the players position and the skeleton's position. (If less than 10 chase player) AND if the angle(field of vision) is less than 30
        {
            direction.y = 0; //this takes the y value out of the equation, (thus the skeleton no longer leans when we approach him/wallk on top of him.)

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); //rotate the skeleton towards the player's direction

            anim.SetBool("isIdle", false); //skeleton will walking towards us, (turn idle off)
            if (direction.magnitude > 5) //if the direction vector(the line between the skeleton andthe player) is of magnitude(length) greater than 5
            {
                this.transform.Translate(0, 0, 0.05f); //the z direction is the foward axis, translate is going to push him about this axis(towards the player)
                anim.SetBool("isWalking", true); //skeleton is now walking (turn walking on)
                anim.SetBool("isAttacking", false); //if the magnitude is greater than 5 the skeleton can't attack us
            }
            else
            {
                anim.SetBool("isAttacking", true);//the skeleton is close enough to attack us
                anim.SetBool("isWlaking", false);//Note: remember to turn past animations off in order to enable new ones 
            }

        }
        else
        {
            anim.SetBool("isIdle", true); //the skeleton is too far to notice us
            anim.SetBool("isWalking", false); //the skeleton can't walk towards us since it's too far
            anim.SetBool("isAttacking", true); //I'm not sure why we would want this on
        }
	}
}
