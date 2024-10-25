using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManeger : MonoBehaviour
{

    public static InventoryManeger instance;
    [SerializeField] InventorySlot[] inventory; 
    public GameObject inventoryItemPrefab;
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);
    }




    private void Start()
    {
        EventSystem.instance.pickupItem += AddItem;
    }

    

    public void InitilizeInv(object item, int index)
    {
        SpawnNewItem((Item)item, inventory[index]);
    }

    public void AddItem(object sender, Item item)
    {
        for(int i = 0;i < inventory.Length;i++)
        {
            InventoryItem itemInSlot = inventory[i].GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null) 
            {
                Debug.Log("EmptySlotFound");
                EventSystem.instance.ModifyInv(item, i);
                SpawnNewItem(item, inventory[i]);
                return;
            }
        }
    }
    

    public void SpawnNewItem(Item item, InventorySlot slot) 
    {
        Debug.Log("Spawning New Item");
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItem.GetComponent<InventoryItem>();
        inventoryItem.InitItem(item);
    }
}
