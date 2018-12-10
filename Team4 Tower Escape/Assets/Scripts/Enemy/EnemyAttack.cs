using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    //private variables
    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health so that the enemy can damage the player. (we have the enemy referencing a script that we have crerated that is on a different gameObject the Player)
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack. (makes sure the enemy isn't attacking too fast or too slow)


    void Awake() //We store and do this in the awake function so we don't have to do it every frame since we don't need to 
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player"); //locates the player for us and store that referece locally
        playerHealth = player.GetComponent<PlayerHealth>(); //this a very inefficient call so we want to do it in the awake function .the we use that player referencethat we just found and too pull the playerHealth script of the player and make a reference to it. (Now we can call the public function takeDamage)
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>(); //set up a reference to our enemies animator component
    }


    void OnTriggerEnter(Collider other) //remember the sphere collider we added to our enemy (other is whatever else collide with the enemy's sphere collider, This will be the player)
    {
        // If the entering collider is the player...
        if (other.gameObject == player) //make sure that other is the player
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other) //inverse of the above function, tells us that something was in the trigger and it has now gone away
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }


    void Update() //The actual attacking happens in the update
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
