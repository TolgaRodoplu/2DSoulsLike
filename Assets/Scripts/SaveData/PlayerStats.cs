using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistance
{
    float playerHealthTotal = 100;
    [SerializeField] float playerHealth = 100;
    public string lastBonfire = string.Empty;
    Item[] playerInventory = new Item[23];

    private void Start()
    {
        EventSystem.instance.modifyInv += ModifyInv;
        EventSystem.instance.healtEdited += EditHealth;
        EventSystem.instance.restorePlayer += Restore;
        EventSystem.instance.EditHealthUI(playerHealth / playerHealthTotal);
    }

    private void InitInventory()
    {
        for(int i = 0;i < playerInventory.Length;i++) 
        {
            if (playerInventory[i] != null)
                InventoryManeger.instance.InitilizeInv(playerInventory[i], i);
        }
        
    }
    
    void Restore()
    {
        playerHealth = playerHealthTotal;
        EventSystem.instance.EditHealthUI(playerHealth / playerHealthTotal);
    }

    void EditHealth(object sender, float change) 
    {
        Debug.Log(playerHealth);
        playerHealth += change;

        if (playerHealth <= 0)
            EventSystem.instance.Death();
        
        EventSystem.instance.EditHealthUI(playerHealth / playerHealthTotal);

        
    }

    private void ModifyInv(object item, int index)
    {
        Item newItem = (Item)item;
        playerInventory[index] = newItem;
    }

    public void LoadData(ref GameData data)
    {
        Item[] folderItems = Resources.LoadAll<Item>("ScriptableObjects/Items");

        playerHealth = data.playerHealth;   


        lastBonfire = data.lastBonfire;

        for(int i = 0; i < playerInventory.Length ; i++)
        {
            if (data.inventoryItems.ContainsKey(i)) 
            {
                foreach (Item item in folderItems)
                {
                    if(item.id == data.inventoryItems[i])
                        playerInventory[i] = item;
                }
            }
            else
                playerInventory[i] = null;
        }



        

        InitInventory();
    }

    public void SaveData(ref GameData data)
    {
        data.playerHealth = playerHealth;
        data.inventoryItems.Clear();
        for (int i = 0; i < playerInventory.Length; i++)
        {
            if (playerInventory[i] != null)
            {
                data.inventoryItems.Add(i, playerInventory[i].id);
            }
        }
    }
}
