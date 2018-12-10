using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target; //a target for the camera to follow
    public float smoothing = 5; //gives the camera a little lag so it doesn't seem as sharp

    Vector3 offset; //private var, use this to store the offset (distance) from the camera to the player

    void Start()
    {
        offset = transform.position - target.position; //calculates the distance from the target and the camera   
    }

    void FixedUpdate() //remember this follows every physics step
    {
        Vector3 targetCamPos = target.position + offset; //target position for the camera, the position will be the position of our target (the player) and add the offset distance in between
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime); //moves the camera using lerp, we want to move  the camera between its current position and the target position we've just made
    }

}
