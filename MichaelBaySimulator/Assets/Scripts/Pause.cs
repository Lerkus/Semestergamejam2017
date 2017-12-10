using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject PausePanel;
    public GameObject[] otherGUI;
    public Points_master pointsMaster;

    public bool paused;

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
        setOtherGUIActive(false);
        paused = true;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        setOtherGUIActive(true);
        Time.timeScale = 1;
        paused = false;
    }

    public void MainMenu()
    {
        if (pointsMaster != null)
        {
            pointsMaster.SetSlavesQuitting();
        }
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    void setOtherGUIActive(bool active)
    {
        for (int i = 0; i < otherGUI.Length; i++)
        {
            otherGUI[i].SetActive(active);
        }
    }
}
