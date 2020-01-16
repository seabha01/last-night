/*  * DialougeUI.cs  * 3DProject  * Hannah Seabert, Luba Grynyshin, David Ross, Thomas Mallick, Caroline Henning  * Apr-23-2019  *   * Script to handle implementation of VIDE dialogue system  */  using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; using VIDE_Data;  public class DialogueUI : MonoBehaviour {     public GameObject npc_container;     public GameObject player_container;     public Text npc_text;     public Text[] text_choices;     private PlayerController DialogueInPause;     public GameObject escapeButton;      // Start is called before the first frame update     void Start()     {         // UI containers should both be deactivated by default (we don't want to see them)         npc_container.SetActive(false);         player_container.SetActive(false);         escapeButton.SetActive(false);          DialogueInPause = GameObject.FindObjectOfType<PlayerController>();     }      // Begin() will trigger the dialogue to start     public void Begin(VIDE_Assign curDialogue)     {
        // pause player control while talking
        if (DialogueInPause != null)         {
            if (DialogueInPause.clipSource.isPlaying)
            {                 DialogueInPause.clipSource.Stop();             }             DialogueInPause.enabled = false;         }          VD.OnNodeChange += UpdateUI;         VD.OnEnd += End;          // Begin dialogue & specify dialogue         VD.BeginDialogue(curDialogue);     }      // UpdateUI handles changes in node data     private void UpdateUI(VD.NodeData data)     {
        // disable UI containers as default
        npc_container.SetActive(false);         player_container.SetActive(false);          // check if data belongs to an NPC or Player node, activate correct container         if (data.isPlayer)         {             player_container.SetActive(true);              // activate buttons corresponding to player choices             for (int i = 0; i < text_choices.Length; i++)             {                 if (i < data.comments.Length)                 {                     text_choices[i].transform.parent.gameObject.SetActive(true);                     text_choices[i].text = data.comments[i];                 }                 else                 {                     text_choices[i].transform.parent.gameObject.SetActive(false);                 }             }         }         else         {             npc_container.SetActive(true);              // set text for npc text element using node data             npc_text.text = data.comments[data.commentIndex];         }     }

    // End handles deactivation of dialogue
    private void End(VD.NodeData data)     {
        // unpause player control
        if (DialogueInPause != null)         {             DialogueInPause.enabled = true;         }          VD.OnNodeChange -= UpdateUI;         VD.OnEnd -= End;         VD.EndDialogue();          // disable UI containers  & dialogue escape button @ conclusion of dialogue         HideEscapeButton();          if (npc_container != null && player_container != null)
        {
            npc_container.SetActive(false);
            player_container.SetActive(false);
        }     }      // handles ending dialogue in case of errors     private void OnDisable()     {         // unpause player control         if (DialogueInPause != null)
        {
            DialogueInPause.enabled = true;
        }          if (npc_container != null)         {             End(null);         }     }      // SetPlayerChoice() sets the player choice as one of the buttons (to be called by OnClick)     public void SetPlayerChoice(int choice)     {         VD.nodeData.commentIndex = choice;          if (Input.GetMouseButtonUp(0))         {             VD.Next();         }     }      // continue after NPC comments have been made (called from OnClick)     public void ContinueButton()     {         if (Input.GetMouseButtonUp(0))         {             VD.Next();         }     }      // EscapeButton() will be called from the "esc" button (OnClick) to end the dialogue     public void EscapeButton()
    {
        if (Input.GetMouseButtonUp(0))         {             End(VD.nodeData);         }
    }      // ShowEscapeButton() activates the dialogue escape button     public void ShowEscapeButton()
    {
        escapeButton.SetActive(true);
    }      // HideEscapeButton() deactivates the dialogue escape button     private void HideEscapeButton()
    {
        if (escapeButton != null)
        {
            escapeButton.SetActive(false);
        }
    }

    // EscapeDialogue() ends the dialogue -- will be used in NPC controller
    public void EscapeDialogue()
    {
        End(VD.nodeData);
    }
} 