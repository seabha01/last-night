/*  * NPCController.cs  * 3DProject  * Hannah Seabert, Luba Grynyshin, David Ross, Thomas Mallick, Caroline Henning  * Apr-23-2019  *   * Script to handle NPC dialogue behavior, determines whether player is in range to begin dialogue  */  using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using VIDE_Data;   public class NPCController : MonoBehaviour {
    private Vector3 threshhold = new Vector3(0, 3.5f, 0);   // for camera to look slightly above cur npc position     private DialogueUI dialogueUI;     private bool inRange = false;                           // keeps track of whether player is in range of NPC     private bool met = false;                               // has player met (first time) NPC already?     private VIDE_Assign assigned;                            // npc-specific dialogue     private string npcName;     private bool stopTalk = false;                          // prevents looping dialogue     private bool doNotEngage = false;                       // when player has everything they need, do not converse anymore     PlayerInventory playerInventory;     public GameObject eChat;      // Start is called before the first frame update     void Start()     {         dialogueUI = FindObjectOfType<DialogueUI>();         assigned = GetComponent<VIDE_Assign>();
        npcName = this.gameObject.tag;         playerInventory = new PlayerInventory();         eChat.SetActive(false);     }

    // Update is called once per frame
    void Update()     {
        // check if player is in range of npc && if they should engage (task isn't completed)
        if (inRange && !doNotEngage)         {             if (!VD.isActive)
            {
                eChat.SetActive(true);                  // check if there is currently an active dialogue                 if (Input.GetKeyDown(KeyCode.E) && !stopTalk)                 {                     eChat.SetActive(false);                     SetStartNode(assigned);                     Camera.main.transform.LookAt(transform.position + threshhold); // look @ npc during conversation                     dialogueUI.Begin(assigned);                     met = true;                     stopTalk = true;                 }
            }         }
    }

    // OnTriggerStay sets inRange to true if Player triggers NPC collider
    private void OnTriggerStay(Collider other)     {         if (other.gameObject.tag == "Player")         {             inRange = true;         }     }

    // OnTriggerExit sets inRange and stopTalk to false if Player exits NPC collider
    private void OnTriggerExit(Collider other)     {         if (other.gameObject.tag == "Player")         {             inRange = false;             stopTalk = false;              if (eChat != null)
            {
                eChat.SetActive(false);
            }         }     }

    // SetConditions sets the start node of the current dialogue based on:
        // 1. ObjectFound() ?
        // 2. met ?
        // 3. else adjust accordingly
    private void SetStartNode(VIDE_Assign dialogue)     {
        if (ObjectFound())         {             if (npcName == "Bartender")             {                 dialogue.overrideStartNode = 6;                 doNotEngage = true;             }             else if (npcName == "Angry")             {                 dialogue.overrideStartNode = 16;             }             else if (npcName == "CoatCheck")             {                 dialogue.overrideStartNode = 5;                 doNotEngage = true;             }             else if (npcName == "Drunk")             {                 dialogue.overrideStartNode = 9;             }         }         else if (!met)         {             return;         }         else         {             if (npcName == "Bartender")             {                 dialogue.overrideStartNode = 15;             }             else if (npcName == "Angry")             {                 dialogue.overrideStartNode = 12;             }             else if (npcName == "CoatCheck")             {                 dialogue.overrideStartNode = 4;             }             else if (npcName == "Drunk")             {                 dialogue.overrideStartNode = 5;             }             else if (npcName == "RandomPartier")             {                 dialogue.overrideStartNode = 7;             }             else if (npcName == "RandomPartier2")             {                 dialogue.overrideStartNode = 9;             }             else if (npcName == "Sitting")             {                 dialogue.overrideStartNode = 10;             }             else if (npcName == "Bouncer")             {                 doNotEngage = true;             }         }     }

    // ObjectFound() returns true if object has been found, false otherwise
    private bool ObjectFound()     {         ArrayList inventory = playerInventory.GetInventory(); 
        // bouncer, random partiers, sitting, do not need objects found
        if (inventory == null)         {             return false;         }         else if (npcName == "Bouncer" || npcName.Contains("RandomPartier") || npcName == "Sitting")         {             return false;         }         else if (npcName == "Bartender" || npcName == "Angry")         {             if (inventory.Contains("Wallet"))             {                 return true;             }         }         else if (npcName == "CoatCheck")         {             if (inventory.Contains("Ticket"))             {                 return true;             }         }         else if (npcName == "Drunk")         {             if (inventory.Contains("Phone"))             {                 return true;             }         }         return false;     } }