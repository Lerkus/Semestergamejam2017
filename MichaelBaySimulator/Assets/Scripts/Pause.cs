using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;

    bool paused;

	// Use this for initialization
	void Start () {
        PausePanel.SetActive(false);
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            } else
            {
                PauseGame();
            }
        }
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        paused = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
