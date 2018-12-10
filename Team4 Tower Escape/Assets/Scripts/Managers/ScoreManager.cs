using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour { //very simple manager class, the score isn't actually accumulated here
    public static int score;        // The player's score. 
    //(a static variable  doesn't belong to the instance of the class it belongs to the class itself)

    Text text;                      // Reference to the Text component.

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "Score: " + score;
    }
}
