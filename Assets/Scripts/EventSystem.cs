using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;
    public event EventHandler<int> inventoryInit;

    public event EventHandler<bool> playerSet, toggleInventory, toggleInteractText;
    public event EventHandler<Item> pickupItem;
    public event EventHandler<int> modifyInv;
    public event EventHandler<string> changeInteractText;
    public event Action openTeleportMenu, gameSaved, restorePlayer, death;
    public event EventHandler<float> editHealthUI, healtEdited;

    private void Awake()
    {
        if (instance != null) 
            Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void RestorePlayer()
    {
        restorePlayer?.Invoke();
    }

    public void SetPlayer(bool active)
    {
        playerSet?.Invoke(this, active);
    }

    public void ToggleInventory(bool active)
    {
        toggleInventory?.Invoke(this, active);
    }

    public void AddItem(Item item)
    {
        pickupItem?.Invoke(this, item);
    }

    public void ModifyInv(Item item, int index) 
    {
        modifyInv?.Invoke(item, index);
    }

    public void InitilizeInventory(Item item, int index)
    {
        Debug.Log("InventoryIniting " + item.name + " " + index);
        
        inventoryInit?.Invoke(item, index);
    }

    public void ToggleInteractText(bool active)
    {
        toggleInteractText?.Invoke(this, active);
    }
    public void ChangeInteractText(string txt)
    {
        changeInteractText?.Invoke(this, txt);
    }

    public void OpenTeleportUI()
    {
        openTeleportMenu?.Invoke();
    }
    
    public void GameSaved()
    {
        gameSaved?.Invoke();
    }
    
    public void EditHealth(float damage)
    {
        healtEdited?.Invoke(this, damage);
    }

    public void EditHealthUI(float leftHealth)
    {
        Debug.Log("EditingHealth");
        editHealthUI?.Invoke(this, leftHealth);
    }

    public void Death()
    {
        SetPlayer(false);
        death?.Invoke();
    }
}
