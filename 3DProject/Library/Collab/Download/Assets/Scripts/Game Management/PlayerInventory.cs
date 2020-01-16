/*
 * PlayerInventory.cs
 * 3D Project
 * Hannah Seabert, Caroline Henning, Thomas Mallick, David Ross, Luba Grynyshin
 * Apr-26-2019
 * 
 * Script to manage player inventory: Add items, get inventory list, display inventory (for testing)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance = null;
    private static ArrayList playerInventory;       // collection of items player has found
    public int numItemsMissing;

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

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = new ArrayList(numItemsMissing);  
    }

    // AddItem takes a string item and adds it to the player inventory
    public void AddItem(string item)
    {
        playerInventory.Add(item);
    }

    // GetInventory returns inventory if not null, else return null
    public ArrayList GetInventory()
    {
        if (playerInventory != null)
        {
            return playerInventory;
        }
        else return null;
    }

    // Size() returns the length of player inventory if it is not null, otherwise return 4 (for purpose of this game)
    public int Size()
    {
        if (playerInventory != null)
        {
            return playerInventory.Capacity;
        }
        else return numItemsMissing;
    }

    // NumItems() returns the current number of items in the inventory if !null, 0 otherwise
    public int NumItems()
    {
        if (playerInventory != null)
        {
            return playerInventory.Count;
        }
        else return 0;
    }

    // InventoryFull() returns true if the player inventory contains all items, false otherwise
    public bool InventoryFull()
    {
        if (playerInventory != null && (playerInventory.Count == playerInventory.Capacity))
        {
            return true;
        }
        else return false;
    }

    // ResetInventory() creates a new empty player inventory
    public void ResetInventory()
    {
        playerInventory = new ArrayList(numItemsMissing);
    }
}
