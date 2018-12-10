using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    public float damagePerShot = 20f;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot. controls (how quickly our gun can fire, reduce to increase fire) rate
    public float range = 100f;                      // The distance the gun can fire. (how far our bullets will travel)

    //private variables
    float timer;                                    // A timer to determine when to fire. keeps everything in sync
    Ray shootRay = new Ray();                       // A ray from the gun end forwards. (use this to figure out what exactly we hit)
    RaycastHit shootHit;                            // A raycast hit to get information about what was hit. (return back to us what we hit)
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer. (to make sure we can only hit shootable things)
    ParticleSystem gunParticles;                    // Reference to the particle system. 
    LineRenderer gunLine;                           // Reference to the line renderer. 
    AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    public Light faceLight;                         // Duh 
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.


    void Awake()
    {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable"); //this will return to us the number of our shootable layer. will allows us to shoot anything on the shootable layer

        // Set up the references.
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        faceLight = GetComponentInChildren<Light> (); //why did they add a face light?
    }


    void Update() //where we control wheter or not it's time to shoot
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime; //will increase in size as time progresses

        // If the Fire1 button is being press and it's time to fire (our fire rate allows us to shoot our next round)...
        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) //fire1 is an input axis built for us by unity, it's mapped to left ctrl or left mouse click by default
        {
            // ... shoot the gun.
            Shoot(); //call the shoot function
        }
           
        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime) 
        {
            // ... disable the effects.
            DisableEffects(); //turn on firing effects after enough time has passed after our last shot
        }
    }

    public void DisableEffects()
    {
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        //faceLight.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot() //this is where we do the physics of actually firing the bullets
    {
        // Reset the timer.
        timer = 0f; //reset the amount of time we have to wait before we fire

        // Play the gun shot audioclip.
        gunAudio.Play();

        // Enable the lights.
        gunLight.enabled = true;
        //faceLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        gunParticles.Stop(); //this prevents us from having a visual disconnect between the particle visuals the raycast physics for that shot
        gunParticles.Play();

        // Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true; //remember we unchecked the component bc we don't start firing until the we want to shoot an enemy
        gunLine.SetPosition(0, transform.position); //lines have to start and point somewhere, This is our first position (0 means the first position of the line, transform.position is the barrel of our gun since the script is attached to our gun barrel)

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position; //starts at the tip of the gun
        shootRay.direction = transform.forward; //the raycast will travel fowards down the z-axis (This seems like a very simplistic way of shooting, no vertical aim)

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) //if we hit something within range of our shootable mask
        {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null) //if what we shot does indeed have an enemyhealth script, i.e. not null
            {
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot, shootHit.point); //damagePerShot is our int amount, and shootHit.point is our Vector3 hitPoint parameters in our public TakeDamage function inside of the EnemyHealth script.
            }

            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1, shootHit.point); //1 is our second(end) point on our line and shootHit.point the object that we hit with our raycast
        }
        // If the raycast didn't hit anything on the shootable layer...
        else
        {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range); //draw a line to the end of the bullets range
        }
    }

}
