using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour {

    public PowerUpController controller; //should I set this to public?

    [SerializeField]
    private PowerUp powerUp; //The power power up we'll be referencing

    private Transform transform_; //for performance reasons(this will cache it so we don't constantly use get component to get transform)

	private void Awake () {
        transform_ = transform;
		
	}

    private void OnTriggerEnter(Collider other) //so that we interact with our player
    {
        if (other.gameObject.tag == "Player") //since we're only using one player only the player will have this tag which we're checking(only he needs to activate powerUps)
        {
            ActivatePowerUp();
            gameObject.SetActive(false); //so that the player on grabs the power up once
        }
    }

    private void ActivatePowerUp()
    {
        controller.ActivatePowerUp(powerUp);
    }

    public void SetPowerUp(PowerUp powerUp)
    {
        this.powerUp = powerUp; //as you would in a constructor
        gameObject.name = powerUp.name; //


    }
}
