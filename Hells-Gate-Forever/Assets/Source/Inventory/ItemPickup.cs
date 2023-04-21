using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HellsGate.Inventory;
using HellsGate.PlayerCharacter;
using HellsGate.Manager;


public class ItemPickup : MonoBehaviour
{
    // Public Variables
    public Item item;
    public ItemQuality AutoQuality;

    // Private Variables
    private bool isPickUpActive = false;
    private GameObject playerRef = null;

#if UNITY_EDITOR
    // Start is called before the first frame update
    void Generate()
    {
        var items = Item.GetAllInstances<Item>().ToList().FindAll(x => x.Quality == AutoQuality);
        Debug.Log(items.Count);
        item = items[Random.Range(0, items.Count - 1)];
    }

    public bool AutoGenerate;

    // You can have multiple booleans here
    private void OnValidate()
    {
        if (AutoGenerate)
        {
            // Your function here
            Generate();

            //When its done set this bool to false
            //This is useful if you want to do some stuff only when clicking this "button"
            AutoGenerate = false;
        }
    }
#endif

    void Start()
    {
        // if (AutoGenerate)
        // {
        // var items = Item.GetAllInstances<Item>().ToList().FindAll(x => x.Quality == AutoQuality);
        // Debug.Log(items.Count);
        // item = items[Random.Range(0, items.Count - 1)];
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickUpActive && playerRef != null)
        {
            if (this.playerRef.GetComponent<PlayerCharacterInputManager>().ItemPickUp)
            {
                this.isPickUpActive = false;
                this.playerRef.GetComponent<PlayerCharacterStats>().IsPickupActive = isPickUpActive;
                this.playerRef.GetComponent<PlayerCharacterInventoryManager>().AddItem(item);
                Object.Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.isPickUpActive = true;
            other.gameObject.GetComponent<PlayerCharacterStats>().IsPickupActive = isPickUpActive;
            this.playerRef = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.isPickUpActive = false;
            other.gameObject.GetComponent<PlayerCharacterStats>().IsPickupActive = isPickUpActive;
            this.playerRef = null;
        }
    }
}
