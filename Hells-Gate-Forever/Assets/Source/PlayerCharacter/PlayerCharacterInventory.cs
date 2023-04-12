using System;
using System.Collections;
using System.Collections.Generic;
using HellsGate.Inventory;
using UnityEngine;

public class PlayerCharacterInventory : MonoBehaviour
{
    // =========== Constants ===========
    #region Constants
    [SerializeField] private const int BASE_INVENTORY_SIZE = 24;
    #endregion

    // =========== Inventory ===========
    #region Inventory
    public int InventorySize { get; private set; }
    public Item[] Inventory { get; private set; }
    #endregion

    // =========== Unity Methods ===========
    #region Unity Methods
    private void Awake()
    {
        InventorySize = BASE_INVENTORY_SIZE;
        Inventory = new Item[InventorySize];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    // =========== Inventory Methods ===========
    #region Inventory Methods
    public void AddItem(Item item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = item;
                break;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                Inventory[i] = null;
                break;
            }
        }
    }

    public void RemoveItem(int index)
    {
        Inventory[index] = null;
    }

    public void SwapItems(int index1, int index2)
    {
        Item temp = Inventory[index1];
        Inventory[index1] = Inventory[index2];
        Inventory[index2] = temp;
    }
    #endregion
}
