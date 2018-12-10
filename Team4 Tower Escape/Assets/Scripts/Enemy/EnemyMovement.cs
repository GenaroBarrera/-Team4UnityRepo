using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //This is for the nav mesh/AI path finding

public class EnemyMovement : MonoBehaviour {

    Transform player;               // Reference to the player's position.
    PlayerHealth playerHealth;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.


    void Awake()
    {
        // Set up the references.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
       enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>(); //pulll a reference to the nav mesh we have in the editor
    }


    void Update() //Notice we use update here instead of fixed update, that's bc we're not keeping in time with physics
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) // If the enemy and the player have health left...
        {
            nav.SetDestination(player.position); //... set the destination of the nav mesh agent to the player. (This creates an error if set when the (player or enemy?) is killed. uncomment the code to get rid of this error.)
        }
        else // Otherwise...
        {
            nav.enabled = false; // ... disable the nav mesh agent. the enemy can't move if it's dead or if it has no player alive to set a destination to
        }
    }
}
