using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActions : MonoBehaviour {
    [SerializeField]
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;
    private PlayerShooting playerShooting;

    //Increase speed power up behaviour
    public void HighSpeedStartAction()
    {
        playerMovement.speed *= 2; //this will increase the player's speed by a multiple of 2 (twice as fast)
    }

    public void HighSpeedEndAction()
    {
        playerMovement.speed = playerMovement.defaultSpeed; //this resets the player's speed
    }

    //increase health power up behaviour
    public void HealthBoostStartAction()
    {
        playerHealth.currentHealth += 25; //give the player 25% health

    }

    public void HealthBoostEndAction()
    {
        playerHealth.currentHealth += 0; //the player's health should keep the boost/stay the same 
    }

    //increase fire rate speed
    public void FireRateBoostStartAction()
    {
        playerShooting.timeBetweenBullets = .07f; //make the player's gun shoot twice as fast

    }

    public void FireRateBoostEndAction()
    {
        playerShooting.timeBetweenBullets = playerShooting.defaultTimeBetweenBullets; //reset the fire rate to the default speed 
    }

    //increase damage
    public void DamageBoostStartAction()
    {
        playerShooting.damagePerShot = 40; //increase damage twice over
    }

    public void DamageBoostEndAction()
    {
        playerShooting.damagePerShot = playerShooting.defaultDamagePerShot; //reset damage per shor to the default value
    }
    
}
