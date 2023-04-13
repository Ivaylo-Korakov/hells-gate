using System;
using System.Collections;
using System.Collections.Generic;
using HellsGate.Inventory;
using UnityEngine;

public class PlayerCharacterInventoryManager : MonoBehaviour
{
    // =========== Constants ===========
    #region Constants
    [SerializeField] private const int BASE_INVENTORY_SIZE = 32;
    #endregion

    // =========== Inventory ===========
    #region Inventory
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    private int selectedSlot = -1;
    #endregion

    // =========== Unity Methods ===========
    #region Unity Methods
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        this.ChangeSelectedSlot(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    // =========== Inventory Methods ===========
    #region Inventory Methods
    public void ChangeSelectedSlot(int index)
    {
        if (this.selectedSlot != -1)
        {
            this.inventorySlots[this.selectedSlot].Deselect();
        }

        this.selectedSlot = index;
        this.inventorySlots[this.selectedSlot].Select();
    }

    public bool AddItem(Item item)
    {
        if (item.IsStackable)
        {
            for (int i = 0; i < this.inventorySlots.Length; i++)
            {
                InventorySlot inventorySlot = this.inventorySlots[i];
                InventoryItem itemInSlot = inventorySlot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null &&
                    itemInSlot.Item == item &&
                    itemInSlot.Count < item.MaxStackSize)
                {
                    itemInSlot.Count++;
                    itemInSlot.RefreshCount();
                    return true;
                }
            }
        }

        for (int i = 0; i < this.inventorySlots.Length; i++)
        {
            InventorySlot inventorySlot = this.inventorySlots[i];
            InventoryItem itemInSlot = inventorySlot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                this.SpawnNewItem(item, inventorySlot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot inventorySlot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, inventorySlot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public Item GetSelectedItem(bool use = false)
    {
        if (this.selectedSlot == -1) return null;

        InventorySlot inventorySlot = this.inventorySlots[this.selectedSlot];
        InventoryItem itemInSlot = inventorySlot.GetComponentInChildren<InventoryItem>();

        if (itemInSlot != null)
        {
            Item item = itemInSlot.Item;
            if (use)
            {
                itemInSlot.Count--;
                if (itemInSlot.Count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

    public void RemoveItem(int index)
    {

    }
    #endregion
}
