using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Destroy(gameObject, 1.5f); //destroy the effect after it's been instansiated for 1.5sec
        
	}
}
