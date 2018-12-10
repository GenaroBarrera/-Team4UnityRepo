using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateScript : MonoBehaviour { //Note: comments are from copied class, go to Highspeed

    public GameObject pickupEffect; //reference to our effect

    public float multiplier = 2; //our power up multiplier (so it isn't hardcoded)
    public float duration = 5;   //puwer up effect duration

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other)); //our player is the "other" object as proven by the if statement
        }
    }

    IEnumerator Pickup(Collider player) //we call our param palyer, since at this point we konw that the player has picked up the power up
    {
        //Debug.Log("Power up picked up!");

        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation); //spawn the effect on the power up's exact position

        //Apply poewr up to the player
        PlayerShooting playerShooting = player.GetComponentInChildren<PlayerShooting>(); //NOTE: we must use get component in children since the plauerShooting script is not in the player itself, but on his gun barrel end
        playerShooting.timeBetweenBullets /= multiplier; //now we can modify this variable in the PlayerMovement script using playerMovement

        //Disable the power up gameObject's graphics and collider once picked up(don't destroy it yet)
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

        //wait x amount of seconds(using the coroutine's functionality)
        yield return new WaitForSeconds(duration);

        //then reverse the effect on our player
        playerShooting.timeBetweenBullets *= multiplier; //reset the player's speed

        //Remove power up
        Destroy(gameObject); //destroys this power up aafter it's been picked up
    }
}
