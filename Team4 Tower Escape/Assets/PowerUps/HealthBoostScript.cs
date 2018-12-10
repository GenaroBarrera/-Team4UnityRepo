using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostScript : MonoBehaviour {

    public GameObject pickupEffect; //reference to our effect

    public int addHealth = 25; //add 25 points of health to our player

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other); //our player is the "other" object as proven by the if statement
        }
    }

    void Pickup(Collider player) //we call our param palyer, since at this point we konw that the player has picked up the power up
    {
        //Debug.Log("Power up picked up!");

        //Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation); //spawn the effect on the power up's exact position

        //Apply poewr up to the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); //Get a reference to our PlayerMovement script and store it as playerMovement(Note: player.)
        playerHealth.currentHealth += addHealth; //now we can modify this variable in the PlayerMovement script using playerMovement

        //Remove power up
        Destroy(gameObject); //destroys this power up aafter it's been picked up
    }
}
