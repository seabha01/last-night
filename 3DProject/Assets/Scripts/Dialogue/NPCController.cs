/*  * NPCController.cs  * 3DProject  * Hannah Seabert, Luba Grynyshin, David Ross, Thomas Mallick, Caroline Henning  * Apr-23-2019  *   * Script to handle NPC dialogue behavior, determines whether player is in range to begin dialogue  */  using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using VIDE_Data;   public class NPCController : MonoBehaviour {
    private Vector3 threshhold = new Vector3(0, 3.5f, 0);   // for camera to look slightly above cur npc position     private DialogueUI dialogueUI;     private bool inRange = false;                           // keeps track of whether player is in range of NPC     private bool met = false;                               // has player met (first time) NPC already?     private VIDE_Assign assigned;                            // npc-specific dialogue     private string npcName;     PlayerInventory playerInventory;     public GameObject eChat;     private Text eChatText;     public StepParser stepParser;     private bool canEscape = false;      // Start is called before the first frame update     void Start()     {         dialogueUI = FindObjectOfType<DialogueUI>();         assigned = GetComponent<VIDE_Assign>();
        npcName = this.gameObject.tag;         playerInventory = FindObjectOfType<PlayerInventory>();         eChatText = eChat.GetComponentInChildren<Text>();         eChat.SetActive(false);     }

    // Update is called once per frame
    void Update()     {
        // begin dialogue if player is in range of NPC
        if (inRange)
        {
            // add esc key & button functionality if conversation isn't necessary
            if (VD.isActive && canEscape)             {                 dialogueUI.ShowEscapeButton();                  if (Input.GetKeyDown(KeyCode.Escape))
                {
                    dialogueUI.EscapeDialogue();
                }             }             else if (!VD.isActive)
            {                 eChat.SetActive(true);                 eChatText.text = "'E' to chat";                  if (Input.GetKeyDown(KeyCode.E))
                {                     eChat.SetActive(false);                     SetStartNode(assigned);                     Camera.main.transform.LookAt(transform.position + threshhold);                     dialogueUI.Begin(assigned);                     met = true;                 }
            }         }
    }

    // OnTriggerStay sets inRange to true if Player triggers NPC collider
    private void OnTriggerStay(Collider other)     {         if (other.gameObject.tag == "Player")         {             inRange = true;         }     }

    // OnTriggerExit sets inRange to false, disables "E" text when player leaves range of NPC
    private void OnTriggerExit(Collider other)     {         if (other.gameObject.tag == "Player")         {             inRange = false;              if (eChat != null)
            {                 eChatText.text = "'E' for pickup";  // reset to default
                eChat.SetActive(false);
            }         }     }

    // SetConditions sets the start node of the current dialogue based on:
        // 1. does the player have all of the missing items?         // 2. ObjectFound() ?
        // 3. met ?
        // 4. else adjust accordingly
    private void SetStartNode(VIDE_Assign dialogue)     {
        if (playerInventory.InventoryFull())
        {             if (npcName == "Bartender")
            {
                if (stepParser.billPayed)
                {
                    dialogue.overrideStartNode = 20;
                }                 else dialogue.overrideStartNode = 6;
            }             else if (npcName == "CoatCheck")
            {
                dialogue.overrideStartNode = 12;
            }             else if (npcName == "Bouncer")
            {
                if (stepParser.billPayed)
                {
                    dialogue.overrideStartNode = 5;                 }                 else dialogue.overrideStartNode = 6;             }
            else if (npcName == "RandomPartier")             {                 dialogue.overrideStartNode = 10;             }
            else if (npcName == "RandomPartier2")             {                 dialogue.overrideStartNode = 12;             }
            else if (npcName == "Angry")             {                 dialogue.overrideStartNode = 17;             }
            else if (npcName == "Sitting")             {                 dialogue.overrideStartNode = 15;             }
            else if (npcName == "Drunk")             {                 dialogue.overrideStartNode = 10;             }
        }         else if (ObjectFound())         {             if (npcName == "Bartender")             {                 dialogue.overrideStartNode = 6;             }             else if (npcName == "Angry")             {                 dialogue.overrideStartNode = 16;             }             else if (npcName == "CoatCheck")             {                 dialogue.overrideStartNode = 5;             }             else if (npcName == "Drunk")             {                 dialogue.overrideStartNode = 9;             }         }         else if (!met)         {             return;         }         else         {             if (npcName == "Bartender")             {                 dialogue.overrideStartNode = 15;             }             else if (npcName == "Angry")             {                 dialogue.overrideStartNode = 12;             }             else if (npcName == "CoatCheck")             {                 dialogue.overrideStartNode = 4;             }             else if (npcName == "Drunk")             {                 dialogue.overrideStartNode = 5;             }             else if (npcName == "RandomPartier")             {                 dialogue.overrideStartNode = 7;             }             else if (npcName == "RandomPartier2")             {                 dialogue.overrideStartNode = 9;             }             else if (npcName == "Sitting")             {                 dialogue.overrideStartNode = 10;             }         }     }

    // ObjectFound() returns true if object has been found, false otherwise
    private bool ObjectFound()     {         ArrayList inventory = playerInventory.GetInventory(); 
        if (inventory == null)         {             return false;         }         else if (npcName == "Bouncer" || npcName.Contains("RandomPartier") || npcName == "Sitting")         {             return false;         }         else if (npcName == "Bartender" || npcName == "Angry")         {             if (inventory.Contains("Wallet"))             {                 return true;             }         }         else if (npcName == "CoatCheck")         {             if (inventory.Contains("Ticket"))             {                 return true;             }         }         else if (npcName == "Drunk")         {             if (inventory.Contains("Phone"))             {                 return true;             }         }         return false;     }      // CanEscapeChat() sets "canEscape" to true when player/npc first conversation has concluded                  //(called from VIDE @ appropriate time)     public void CanEscapeChat()
    {
        canEscape = true;
    } }