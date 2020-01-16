/*
 * ScoreKeeper.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 25-Apr-2019
 * 
 * Script to keep track of score, the sum of the number of successes at each of the
 * four bottom arrows (Collision Trackers). Attached to Empty GameObject DanceDance Game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    public StepParser stepParser;
    public CollisionTracker[] collisionTrackers;

    public int score;
    public bool displayText;
    public bool giveResult;
    public Text text;

    //enforce singleton pattern
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

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        displayText = false;
        giveResult = false;
    }

    // display score for duration of game and give result at conclusion of dance
    void Update()
    {
        score = collisionTrackers[0].personalScore + collisionTrackers[1].personalScore
            + collisionTrackers[2].personalScore + collisionTrackers[3].personalScore;

        if (displayText)
        {
            if (giveResult)
            {
                GiveResult();
            }
            else
            {
                text.text = "Your Score: " + score + "\n\nScore to Beat: 55";
            }
        }
        else
        {
            text.text = "";
        }
    }

    // dance off is over--tell player how they did
    public void GiveResult()
    {
        if (score < 55)
        {
                text.text = "Wow, that was bad..\nWe'll pretend that you won so we don't" +
                    " have to see that again.";
                stepParser.billPayed = true;
        }
        else if (score >= 55)
        {
            text.text = "Not bad! You have won the dance battle, dance kween.";
            stepParser.billPayed = true;
        }
    }
}
