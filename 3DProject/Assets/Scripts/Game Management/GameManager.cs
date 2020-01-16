/*
 * GameManager.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 24-Apr-2019
 * 
 * Script to handle the overall game. Keeps track of number of items collected
 * and when the game has been completed.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject objUI;
    public StepParser stepParser;
    public GameObject itemCollectedUI;

    public Text infoText;

    private PlayerInventory pi;

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
        //finds and returns the canvas of ObjectNum and CollectedItems
        objUI = GameObject.Find("ObjectNum");
        itemCollectedUI = GameObject.Find("CollectedItems");

        pi = new PlayerInventory();

        //displays text in the beginning of gameplay to provide info to player
        infoText.text = "Woah... what happened last night?\n\n...Oh man, where is all your stuff?";
        Invoke("Confusion", 5f);
    }


    void Update()
    {
        //update the text to canvas as the object count changes
        objUI.GetComponent<Text>().text = "Missing items left: " +
        ItemCollect.objects.ToString();

        //update the text to canvas as the object count changes
        itemCollectedUI.GetComponent<Text>().text = "Item Collected: " +
        ItemCollect.itemList;

        if (pi.InventoryFull()) //all items have been collected
        {
            if (stepParser.billPayed == true) //if returned to bartender with wallet
            {
                objUI.GetComponent<Text>().text = "All objects collected\nIt's time to leave, find the exit!";
                itemCollectedUI.SetActive(false);
            }
            else
            {
                objUI.GetComponent<Text>().text = "Pay your bill at the bar!";
                itemCollectedUI.SetActive(false);
            }
        }
    }

    void Confusion()
    {
        infoText.text = "Find your stuff and get out of this bar!\n\nTry asking around" +
        	" where your things might be.";
        Invoke("WaitToChange", 5f);
    }

    void WaitToChange()
    {
        infoText.text = "";
    }

    void FinalScreen()
    {
        SceneManager.LoadScene(2);
    }
}
