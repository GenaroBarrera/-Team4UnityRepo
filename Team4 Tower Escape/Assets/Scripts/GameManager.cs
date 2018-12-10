using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    float timeInScene;
    int trigger;
    public GameObject conversationPanel;

    // Use this for initialization
    void Start () {
        timeInScene = 0f;
        trigger = 0;
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.CompareTag("ConversationPanel"))
            {
                conversationPanel = go;
            }
        }
        conversationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        timeInScene += Time.deltaTime;
        if (trigger == 0 && timeInScene > 5)
            firstConversation();
    
	}

    void firstConversation()
    {
        conversationPanel.SetActive(true);
    }
}
