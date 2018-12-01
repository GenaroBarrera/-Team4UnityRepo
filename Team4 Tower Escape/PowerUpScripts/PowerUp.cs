using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] //this allows fields to be changed in the unity editor (since we're not using a mono behaviour class)
public class PowerUp {
    [SerializeField] // this allows the field to appear in the unity editor (since we're not using a mono behaviour class)
    public string name;

    [SerializeField]
    public float duration;

    [SerializeField]
    public UnityEvent startAction;

    [SerializeField]
    public UnityEvent endAction;

    public void Start () {
        if (startAction != null)
            startAction.Invoke(); // this makes the startAction run
	}
	
	public void End () {
        if (endAction != null)
            endAction.Invoke(); // this makes the endAction run
    }
}
