using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour {
    public Transform trans;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = trans.position + new Vector3(0.0f, 4.5f, -2.7f);
	}
}
