using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    //public GameObject[] powerUpPrefabs = new GameObject[3]; //make this into an array or list that takes multiple gameobjects
    public GameObject powerUpPrefab; //make this into an array or list that takes multiple gameobjects


    public List<PowerUp> powerUps;

    public Dictionary<PowerUp, float> activePowerUps = new Dictionary<PowerUp, float>(); //this is our hash table/map (let's us store someting by a key and a value)
    
    private List<PowerUp> keys = new List<PowerUp>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleActivePowerUps(); //we only want to run this when the game is playing

        /*
        //spawn using key T
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnPowerUp(powerUps[0], new Vector3(3, .75f, -3));
        }
        */
		
	}

    public void HandleActivePowerUps()
    {
        bool changed = false;

        if (activePowerUps.Count > 0)
        {
            foreach(PowerUp powerUp in keys)
            {
                if (activePowerUps[powerUp] > 0) //is this power up has a duration lasting greater than 0
                {
                    activePowerUps[powerUp] -= Time.deltaTime; //reduce the duration by the change in time since the last tick
                }
                else //if we no longer have any time left
                {
                    changed = true;
                    activePowerUps.Remove(powerUp); //removing spent power ups from the dictionary
                    powerUp.End(); //end the power up effect
                }
            }
        }
        if (changed) // if changed is true
        {
            keys = new List<PowerUp>(activePowerUps.Keys); //refresh the keys in the dictionary
        }
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (!activePowerUps.ContainsKey(powerUp))
        {
            powerUp.Start();
            activePowerUps.Add(powerUp, powerUp.duration);

        }
        else
        {
            activePowerUps[powerUp] += powerUp.duration; //this will allows us to stack powerups (increase their duration)
        }

        keys = new List<PowerUp>(activePowerUps.Keys); 
    }

    public GameObject SpawnPowerUp(PowerUp powerUp, Vector3 position) //make this take an array of power ups?
    {
        GameObject powerUpGameObject = Instantiate(powerUpPrefab); //how to instantiate an array of gameobjects?
        /*
        for (int i = 0;  i < powerUpPrefabs.Length; i ++)
        {
            powerUpPrefabs[i] = Instantiate(powerUpPrefabs[i]) as GameObject;
        }
        */
        
        var powerUpBehaviour = powerUpGameObject.GetComponent<PowerUpBehaviour>();

        powerUpBehaviour.controller = this;

        powerUpBehaviour.SetPowerUp(powerUp); 

        powerUpGameObject.transform.position = position;

        return powerUpGameObject;
    }
}
