using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    public PowerUpController powerUpController;
    public PlayerHealth playerHealth;                   // Reference to the player's heatlh. (We only want to spawn enemies if the player is alive)
    //public GameObject powerUpPrefab;                  // The enemy prefab to be spawned.
    private float powerUpSpawnTime;                     // How long between each spawn.(I'm hoping this is between 0 and 10 seconds)
    public Transform[] powerUpSpawnPoints;              // An array of the spawn points this enemy can spawn from. (how do we add more spawn points into this array?)       
    int index;                                          //make the index of the powerUpPrefabs random to spawn them randomly

    // Use this for initialization
    void Start () {
        powerUpSpawnTime = Random.Range(2, 10);
        InvokeRepeating("Spawn", powerUpSpawnTime, powerUpSpawnTime); //The InvokeRepeating function allows us to repeat a function call without using a timer or loop (call the spawn function as a string, spawnTime is the amount of time to wait before doing it, and an amount of time to wait before repeating it.)
    }

    void Spawn() //spawns enemies
    {
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return; //we don't need to keep spawning power ups
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, powerUpSpawnPoints.Length); //int represents the point in the array Transform[], Random.Range will pick a a random spawnpoint from all the available spawnpoints.

        //(OLD CODE)Instantiate(powerUpPrefab, powerUpSpawnPoints[spawnPointIndex].position, powerUpSpawnPoints[spawnPointIndex].rotation); //syntax(the thing to spawn/instantiate, where to spawn it, and what rotation it should have when created)
        // Create an instance of the powerUpPrefab at the randomly selected powerUpSpawnPoint's position
        //use index to select betwenn random powerups
        index = Random.Range(0, powerUpController.powerUps.Count); //Note: powerUps is in a List<>, in order to get the sizee use Count
        powerUpController.SpawnPowerUp(powerUpController.powerUps[2], powerUpSpawnPoints[spawnPointIndex].position);

    }
}
