using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            words.text = "Unknown Voice: Finally awake, I see.";
        }
        
        if (conversationTrigger == 1)
        {
            words.alignment = TextAnchor.MiddleLeft;
            words.color = Color.white;
            words.text = "Player: Who's there?";
        }

        if (conversationTrigger == 2)
        {
            words.alignment = TextAnchor.MiddleLeft;
            words.color = Color.white;
            words.text = "Player: And where am I?";
        }

        if (conversationTrigger == 3)
        {
            words.alignment = TextAnchor.MiddleLeft;
            words.color = Color.red;
            words.text = "Unknown Voice: You're in the Tower!";
        }

        if (conversationTrigger == 4)
        {
            SceneManager.LoadScene("Floor12", LoadSceneMode.Single); //load the 12th Floor
        }


    }
}
