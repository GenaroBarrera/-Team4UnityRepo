using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenPages : MonoBehaviour {
    public GameObject aboutGameButton;
    public GameObject startGameButton;
    public GameObject creditsButton;
    public GameObject aboutGamePanel;
    public GameObject secondAboutGamePanel;
    public GameObject creditsPanel;
    public GameObject levelSelectPanel;
    public GameObject passwordInputBox;


    int aboutGameCounter;
	// Use this for initialization
	void Start () {
        aboutGameCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AboutGame()
    {
        if(aboutGameCounter == 0)
        {
            aboutGamePanel.SetActive(true);
            aboutGameCounter++;
        }
        else
        {
            aboutGamePanel.SetActive(false);
            secondAboutGamePanel.SetActive(true);
            aboutGameCounter = 0;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
    }

    public void LevelSelect()
    {
        levelSelectPanel.SetActive(true);
    }

    public void MainMenu()
    {
        secondAboutGamePanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void LevelSkip(string password)
    {
        if(password == "level2")
        {
            SceneManager.LoadScene("LevelTwo", LoadSceneMode.Single);
        }
    }
}
