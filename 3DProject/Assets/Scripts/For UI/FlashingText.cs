/*
 * FlashingText.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 14-Apr-2019
 * 
 * Script to make text flash in and out on main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingText : MonoBehaviour
{
    public Text winText;
    public bool textBlink = true;
    public bool startDisplayed = true;
    public float waitTime = 1f;
    public float displayTime = 1f;

    void Start()
    {
        StartCoroutine("FlashText");
    }

    public IEnumerator FlashText()
    {
        while (textBlink)
        {
            if (startDisplayed)
            {
                winText.enabled = true;
                yield return new WaitForSeconds(displayTime);
                winText.enabled = false;
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                winText.enabled = false;
                yield return new WaitForSeconds(waitTime);
                winText.enabled = true;
                yield return new WaitForSeconds(displayTime);
            }
        }
    }
}