using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellsGate.Inventory;
using HellsGate.PlayerCharacter;

public class DemoScriptItems : MonoBehaviour
{
    public PlayerCharacterInventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void Cheat() {
        foreach (Item item in this.itemsToPickup) {
            this.inventoryManager.AddItem(item);
        }
        this.inventoryManager.gameObject.GetComponent<PlayerCharacterStats>().AddGold(9999);
    }

    public void PickupItem(int id)
    {
        this.inventoryManager.AddItem(this.itemsToPickup[id]);
        this.inventoryManager.gameObject.GetComponent<PlayerCharacterStats>().AddGold(1000);
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
