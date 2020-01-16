/*
 * StepParser.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 24-Apr-2019
 * 
 * This script reads in the files used for the rhythm mini-game. The script
 * creates a list of strings where each string in the list represents a coded 
 * drop of arrows (e.g. 0001 means right arrow is dropped. 
 * It allows for reading in different files based on level the player is on.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class StepParser : MonoBehaviour
{
    private string noteStep = "0000";
    public List<string> notes;

    float timeLeft = 10f;
    public Text text;

    public GameObject[] ArrowSpawners;
    public GameObject[] Arrows;
    public PlayerController playerController;
    public ScoreKeeper scoreKeeper;

    public bool letsDance;
    public bool billPayed;

    public static StepParser instance;


    //enforces singleton pattern
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //We couldn't get the file to load in the WebGL build so we hard coded the list used for the Dance Mini-Game.
        //Comment in line 57 and comment out line 58 to see the load script work within the Unity Editor
        // LoadFile(0); 
        notes = new List<string>() { "0000", "0100", "0000", "0100", "0000", "0010", "0000", "0010", "0000", "1000", "0000", "0001", "0000", "0100", "0000", "0101", "0000", "1010", "0000", "0100", "1000", "1000", "0100", "0010", "0001", "0000", "0001", "1000", "0100", "0010", "1001", "0001", "1010", "0010", "0000", "1001", "0110", "1001", "1001", "0110", "0001", "0100", "1000", "0110", "0110", "0001", "0010", "1000", "0000", "1100", "0110", "0011", "1000", "0001", "0100", "0010", "0110", "0001", "1000", "0100", "1000", "0001", "0010", "1000", "0100", "1100", "1001", "0001", "0100", "1010", "0101", "1001", "1010", "1111", "0000", "0000", "0000", "0000" };

        letsDance = false;
        scoreKeeper = GetComponent<ScoreKeeper>();
    }

    // if given the command to start, commence game and disable player controls
    void Update()
    {
        //time to start round one
        if (letsDance)
        {
            timeLeft -= Time.deltaTime;
            text.text = "Dancers, get into position!\nDance-off beginning in:\n"
             + Mathf.Round(timeLeft);

            //time for dance battle
            if (timeLeft < 0)
            {
                DanceOn();
            }
        }
    }


    // call loadfile with incremented lvlCount to move to next file
    void LoadFile(int lvlCount)
    {
        string path = Application.streamingAssetsPath + "/NoteData" + lvlCount.ToString() + ".txt";
        StreamReader reader = new StreamReader(path);

        while (noteStep != null)
        {
            noteStep = reader.ReadLine();
            notes.Add(noteStep);
        }
        reader.Close();
    }

    //read through list of arrowCodes, pausing between each line
    //reenable player controls after game ends
    void DanceOn()
    {
        playerController.enableMovement = false;
        letsDance = false;
        text.text = "";
        scoreKeeper.displayText = true;

        StartCoroutine("CreateDance");
    }

    IEnumerator CreateDance()
    {
        //read in one line of notes list
        for (int i = 0; i < notes.Count - 1; i++)
        {
            Spawn(notes[i]);
            yield return new WaitForSeconds(0.7f);
        }

        scoreKeeper.giveResult = true;
        StartCoroutine("TurnOffDance");
    }

    IEnumerator TurnOffDance()
    {
        yield return new WaitForSeconds(5f);
        scoreKeeper.giveResult = false;
        scoreKeeper.displayText = false;
        playerController.enableMovement = true;
    }

    //spawn corresponding arrows to a string input arrowCode
    void Spawn(string arrowCode)
    {
        for (int i = 0; i < 4; i++)
        {
            if (arrowCode[i] == '1')
            {
                Instantiate(Arrows[i],
                ArrowSpawners[i].transform.position,
                Quaternion.AngleAxis(-90, Vector3.up));
            }
        }
    }

    // StartGame() sets letsDance to true in order to start the mini game 
    // public --> to be called from VIDE action node
    public void StartGame()
    {
        letsDance = true;
    }
}
