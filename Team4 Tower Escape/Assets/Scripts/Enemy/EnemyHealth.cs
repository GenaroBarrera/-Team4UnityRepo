using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public float currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.

    //private variables
    Animator anim;                              // Reference to the animator.
    AudioSource enemyAudio;                     // Reference to the audio source.
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor. (for their sinking animation)


    void Awake() 
    {
        // Setting up the references.
        anim = GetComponent<Animator>(); 
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>(); //Will go through all of the children an return the first ParticleSystem Component
        capsuleCollider = GetComponent<CapsuleCollider>(); 

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth; 
    }


    void Update() //all we do here is check wheter the enemy is sinking or not
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second. (-Vector3.up means down)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(float amount, Vector3 hitPoint) //public function, can be called outside this class by another script (int amount will be used to see how mauch damege is being taken, Vector3 hitpoint will be used to check where the enemy has been hit to move the partleSystem around the enemy)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death() //the death function
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it. (when an enemy dies it no longer is an obstacle and the player can move through them)
        capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead. perform the death animation
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking() //another public function, it's public because we don't call it in this class. instead unity calls it when the gameOnject's Animation takes place. 
    {
        // Find and disable the Nav Mesh Agent. (.SetActive = false is to turn off the whole gameObjects, .enabled = false is to set the component off not the whole gameObject)
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; //Quick Note: since we didn't import Unity.Engine.AI, we have to reference it when we're using AI components

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true; //set to kinematic so unity can ingnore it

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value. 
        ScoreManager.score += scoreValue; //Note how we reference score only using script where it was declared. (We don't create an instance of the script by using getComponent)
        //the reason we don't use static variables for other stuff such as enemies or players is because we wouldn't be able to change their components individually such as their health. We would be changing all of the enemies health. In this case we only have one score we need to worry about.

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}
