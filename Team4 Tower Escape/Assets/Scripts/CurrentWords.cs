using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWords : MonoBehaviour {

    int conversationTrigger = 0;
    Text words;
    bool canProgress;
	// Use this for initialization
	void Start () {
        words = GetComponent<Text>();
        words.text = "";
        canProgress = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetKey(KeyCode.E))
            canProgress = true;
        if (Input.GetKey(KeyCode.E) && canProgress == true)
        {
            conversationTrigger++;
            canProgress = false;
        }
        if (conversationTrigger == 0)
        {
            words.alignment = TextAnchor.MiddleRight;
            words.color = Color.red;
            words.text = "Finally awake, I see.";
        }
        
        if (conversationTrigger == 1)
        {
            words.alignment = TextAnchor.MiddleLeft;
            words.color = Color.white;
            words.text = "Who's there?";
        }

        if (conversationTrigger == 2)
        {
            words.alignment = TextAnchor.MiddleLeft;
            words.color = Color.white;
            words.text = "And where am I?";
        }
    }
}
