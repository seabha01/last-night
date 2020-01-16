using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject eChat;
    Text eChatText;
    PlayerInventory Inventory; 

    // Start is called before the first frame update
    void Start()
    {
        Inventory = new PlayerInventory();

        eChatText = eChat.GetComponentInChildren<Text>();
        eChat.SetActive(false);
    }

    //This makes the "E" popup appear if the player is infront of the door and has collected everything
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Inventory.InventoryFull())
        {
            eChatText.text = "'E' to Exit";
            eChat.SetActive(true);
        }
    }

    //This makes the "E" popup disappear when the player walks away
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eChat.SetActive(false);
            eChatText.text = "'E' to Pickup";
        }
    }

    //This checkes if the player presses E while having everything collected and loads the final screen if so
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetAxis("Interact") > 0 && Inventory.InventoryFull())
        {
            SceneManager.LoadScene(2);
        }
    }
}
