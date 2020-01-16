/*
 * NPCController.cs
 * 3DProject
 * Hannah Seabert, Luba Grynyshin, David Ross, Thomas Mallick, Caroline Henning
 * Apr-23-2019
 * 
 * Script to handle NPC dialogue behavior, determines whether player is in range to begin dialogue
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;


public class NPCController : MonoBehaviour
{
    public Transform npc;  // cur npc position
    private Vector3 target = new Vector3(0, 3.5f, 0);   // for camera to look slightly above cur npc position
    public DialogueUI dialogueUI;
    private bool inRange = false;   // keeps track of whether player is in range of NPC
    private bool met = false; // has player met (first time) NPC already?
    public VIDE_Assign assigned;    // npc-specific dialogue
    private string npcName;
    private bool stopTalk = false;  // prevents looping dialogue
    private bool doNotEngage = false;   // when player has everything they need, do not converse anymore

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        assigned = GetComponent<VIDE_Assign>(); 
        npcName = this.gameObject.tag;

        Debug.Log("This NPC name is " + npcName);
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in range of npc && if they should engage (task isn't completed)
        if (inRange && !doNotEngage)
        {
            target += npc.transform.position;
            // check if there is currently an active dialogues
            if (!VD.isActive && !stopTalk)
            {
                SetStartNode(assigned);
                Camera.main.transform.LookAt(target);   // look @ npc during conversation
                dialogueUI.Begin(assigned);
                met = true;
                stopTalk = true;
            }
        }
    }

    // OnTriggerStay sets inRange to true if Player triggers NPC collider
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    // OnTriggerExit sets inRange and stopTalk to false if Player exits NPC collider
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            stopTalk = false;
        }
    }

    // SetConditions sets the start node of the current dialogue based on:
        // 1. has the player met the NPC before?
        // 2. Does the player have the item the NPC wants?
            // not every NPC needs an item from the player
            // if the player does have what npc needs, set doNotEngage to true to prevent future interactions
    private void SetStartNode(VIDE_Assign dialogue)
    {
        // if NPC has not been met return immediately & start at the beginning
        // Otherwise, otherwise adjust start
        if (!met && npcName != "Bartender"  && npcName != "CoatCheck")  // we do not want to have initial convo if item is found
        {
            return;
        }
        else
        {
            if (npcName == "Bartender")
            {
                if (ItemCollect.playerInventory != null && ItemCollect.playerInventory.Contains("Wallet"))   //ObjectFound()
                {
                    dialogue.overrideStartNode = 6;
                    doNotEngage = true;
                }
                else if (!met)
                {
                    return;
                }
                else
                {
                    dialogue.overrideStartNode = 15;
                }
            }
            else if (npcName == "Drunk")
            {
                if(ItemCollect.playerInventory != null && ItemCollect.playerInventory.Contains("Wallet"))
                {
                    dialogue.overrideStartNode = 16;
                }
                else
                {
                    dialogue.overrideStartNode = 12;
                }
            }
            else if (npcName == "CoatCheck")    // CoatCheck Character still needs to be added to scene
            {
                if (ItemCollect.playerInventory != null && ItemCollect.playerInventory.Contains("Ticket"))
                {
                    dialogue.overrideStartNode = 5;
                    doNotEngage = true;
                }
                else if (!met)
                {
                    return;
                }
                else
                {
                    dialogue.overrideStartNode = 4;
                }
            }
            else if (npcName == "RandomPartier")
            {
                dialogue.overrideStartNode = 7;
            }
        }
    }
}
