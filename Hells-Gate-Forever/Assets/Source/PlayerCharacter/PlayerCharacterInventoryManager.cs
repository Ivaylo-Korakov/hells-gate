using System;
using System.Collections;
using System.Collections.Generic;
using HellsGate.Inventory;

using HellsGate.PlayerCharacter;
using HellsGate.Manager;
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
    public InventorySlot[] characterSlots;
    public InventorySlot[] stashSlots;
    public GameObject inventoryItemPrefab;
    public GameObject itemPopup;
    public bool IsInventoryOpen;

    private GameObject _mainInventoryGroup;
    private GameObject _mainInventoryButton;
    private GameObject _characterInventoryGroup;
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

        // Inventory
        this.IsInventoryOpen = false;
        this.itemPopup = GameObject.FindGameObjectWithTag("ItemPopup");
        this._mainInventoryGroup = GameObject.FindGameObjectWithTag("MainInventory");
        this._mainInventoryButton = GameObject.FindGameObjectWithTag("MainInventoryButton");
        this._characterInventoryGroup = GameObject.FindGameObjectWithTag("CharInventory");
        if (this.itemPopup != null)
        {
            this.itemPopup.SetActive(false);
        }
        if (this._mainInventoryGroup != null)
        {
            this._mainInventoryGroup.SetActive(false);
        }
        if (this._characterInventoryGroup != null)
        {
            this._characterInventoryGroup.SetActive(false);
        }
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

    public void InventoryChanged()
    {
        // Gather all items in character inventory
        List<Item> charItems = new List<Item>();
        for (int i = 0; i < this.characterSlots.Length; i++)
        {
            InventorySlot inventorySlot = this.characterSlots[i];
            InventoryItem itemInSlot = inventorySlot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                charItems.Add(itemInSlot.Item);
            }
        }

        this.GetComponent<PlayerCharacterStats>().ApplyItemsStats(charItems);
    }


    public void InventoryOpen()
    {
        this.IsInventoryOpen = true;
        _mainInventoryButton.SetActive(false);
        _mainInventoryGroup.SetActive(true);
        _characterInventoryGroup.SetActive(true);
        this.GetComponent<PlayerCharacterInputManager>().ShowCursor();
    }

    public void InventoryClose()
    {
        this.IsInventoryOpen = false;
        _mainInventoryButton.SetActive(true);
        _mainInventoryGroup.SetActive(false);
        _characterInventoryGroup.SetActive(false);
        this.GetComponent<PlayerCharacterInputManager>().HideCursor();
    }
    #endregion
}
