using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellsGate.Inventory;

public class DemoScriptItems : MonoBehaviour
{
    public PlayerCharacterInventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        this.inventoryManager.AddItem(this.itemsToPickup[id]);
    }

    public void GetSelectedItem()
    {
        Item item = this.inventoryManager.GetSelectedItem();
        if (item != null)
        {
            Debug.Log("Item selected: " + item.Title);
        }
        else
        {
            Debug.Log("No item selected");
        }
    }
}
