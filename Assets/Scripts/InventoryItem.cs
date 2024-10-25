using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    bool isInInventory = false;
    public Image image;
    [HideInInspector]public Item item;
    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        EventSystem.instance.toggleInventory += SetIsInInventory;
    }

    public void InitItem(Item newItem)
    {
        Debug.Log(newItem.name);

        if(newItem != null) 
        {
            item = newItem;
            image.sprite = item.sprite;
        }
        
    }

    void SetIsInInventory(object sender, bool active) 
    {
        isInInventory = active;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
