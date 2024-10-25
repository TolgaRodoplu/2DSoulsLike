using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int index = -1;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem item = dropped.GetComponent<InventoryItem>();
        EventSystem.instance.ModifyInv(item.item, this.index);
        int otherSlotIndex = item.parentAfterDrag.GetComponent<InventorySlot>().index;
        EventSystem.instance.ModifyInv(null, otherSlotIndex);


        if (transform.childCount > 0)
        {
            
            EventSystem.instance.ModifyInv(transform.GetChild(0).GetComponent<InventoryItem>().item, otherSlotIndex);
            transform.GetChild(0).parent = item.parentAfterDrag;
            
        }

        item.parentAfterDrag = transform;
    }

    
}