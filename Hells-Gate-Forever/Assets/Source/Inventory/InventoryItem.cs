using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using HellsGate.Inventory;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    [Header("UI")]
    public Image image;
    public GameObject countText;

    [HideInInspector] public Item Item;
    [HideInInspector] public int Count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        this.InitializeItem(this.Item);
    }

    public void InitializeItem(Item newItem)
    {
        if (newItem == null) return;
        this.Item = newItem;
        this.image.sprite = newItem.Icon;
        this.RefreshCount();
    }

    public void RefreshCount()
    {
        this.countText.GetComponentInChildren<TMP_Text>().text = this.Count.ToString();
        bool textActive = this.Count > 1;
        this.countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.image.raycastTarget = false;
        this.parentAfterDrag = this.transform.parent;
        this.transform.SetParent(this.transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.image.raycastTarget = true;
        this.transform.SetParent(this.parentAfterDrag);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterInventoryManager>().InventoryChanged();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        GameObject itemPopup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterInventoryManager>().itemPopup;
        itemPopup.GetComponentInChildren<TMP_Text>().text = this.Item.ToString();
        itemPopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        GameObject itemPopup = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterInventoryManager>().itemPopup;
        itemPopup.SetActive(false);
    }
}
