/*
 * ItemCollect.cs
 * 3D Project
 * Luba Grynyshin, Hannah Seabert, Caroline Henning, Thomas Mallick, David Ross
 * 25-Apr-2019
 * 
 * Script to handle missing item collection using colliders, & keep track of player inventory
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
    public static int objects;
    private PlayerInventory playerInventory;
    public static string itemList;
    public AudioClip pickupAudioClip;
    public GameObject eChat;
    public GameObject DrunkManager;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();

        objects = playerInventory.Size() - playerInventory.NumItems();

        itemList = " ";

        eChat.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            eChat.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            eChat.SetActive(true);

            if (gameObject.tag != "NonIntoxicant" && gameObject.tag != "Intoxicant" && Input.GetAxis("Interact")>0)
            {
                //create empty gameobject as item pick up audio source
                GameObject soundObject = new GameObject();
                soundObject.name = "Item Pickup Audio Source";
                soundObject.transform.position = transform.position;

                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                soundObject.GetComponent<AudioSource>().PlayOneShot(pickupAudioClip);

                objects--;
                playerInventory.AddItem(gameObject.tag);

                itemList = gameObject.tag;

                Destroy(gameObject);
            }

            if (Input.GetAxis("Interact") > 0)
            {
                GameObject soundObject = new GameObject();
                soundObject.name = "Item Pickup Audio Source";
                soundObject.transform.position = transform.position;

                AudioSource audioSource = soundObject.AddComponent<AudioSource>();
                soundObject.GetComponent<AudioSource>().PlayOneShot(pickupAudioClip);

                if (gameObject.tag == "NonIntoxicant")
                {
                    DrunkManager.GetComponent<DrunkVisionManager>().drunkness -= 30;
                } 
                else if ( gameObject.tag == "Intoxicant")
                {
                    DrunkManager.GetComponent<DrunkVisionManager>().drunkness += 20;
                }
                eChat.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}

