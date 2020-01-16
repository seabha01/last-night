/*
 * MissingItemSpawner.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, Luba Grynyshin, David Ross
 * 24-Apr-2019
 * 
 * Script to handle spawning of missing item once proper dialogue has been had (will be called form action node)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissingItemSpawner : MonoBehaviour
{
    public GameObject missingItem;      // item to be spawned
    private bool spawned = false;       // prevents multiple clones of one item (excluding advil)
    public GameObject eChat;
    public GameObject DrunkManager;

    // SpawnItem will spawn missing item on call, if it has not already been spawned
    public void SpawnItem()
    {
        Debug.Log("Enter SpawnItem()");

        if (!spawned || gameObject.tag == "NonIntoxicant" || gameObject.tag == "Intoxicant")
        {
            missingItem.GetComponent<ItemCollect>().eChat = eChat;
            missingItem.GetComponent<ItemCollect>().DrunkManager = DrunkManager;
            Instantiate(missingItem, transform.position, transform.rotation);

            if ((missingItem.tag == "NonIntoxicant") || (missingItem.tag == "Intoxicant"))
            {
                spawned = false;
            } 
            else
            {
                spawned = true;
            }
        }
    }
}
