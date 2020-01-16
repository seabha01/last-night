/*
 * UIManager.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 14-Apr-2019
 * 
 * Script to handle UI for the game. Singleton structure that handles pause menu,
 * main menu and final screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] pauseObjects;
    public Button pauseButton;
    private PlayerInventory playerInventory;

    // UIManager object
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowWhenPaused");
        HidePaused();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    //uses pause button to pause and unpause the game
    public void CheckPauseButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    //reloads Level
    public void Reload()
    {
        playerInventory.ResetInventory();
        Invoke("ToReload", 0.2f);
        PauseControl();
    }

    void ToReload()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    //shows objects with ShowWhenPaused tag
    public void ShowPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowWhenPaused tag
    public void HidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(int level)
    {
        if (level == 1)
        {
            StartCoroutine(loadBarScene());
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }

    // to allow for click sound to register before new scene load
    public IEnumerator loadBarScene()
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(1);
    }

    //quits application in which game is running
    //will not quit editor in unity
    public void EndGame()
    {
        StartCoroutine(waitToEnd());
    }

    public IEnumerator waitToEnd()
    {
        yield return new WaitForSeconds(.3f);
        Application.Quit();
    }
}