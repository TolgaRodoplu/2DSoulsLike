using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpInteract : Interactable, IDataPersistance
{
    [SerializeField] private string id;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private Item item;

    private bool pickedUp = false;

    public override void Interact(Transform interactor)
    {
        if (item != null && !pickedUp) 
        {
            EventSystem.instance.AddItem(item);
            pickedUp = true;
            gameObject.SetActive(false);
        }
        
    }

    public void LoadData(GameData data)
    {
        data.pickedUpItems.TryGetValue(id, out pickedUp);


        
        gameObject.SetActive(!pickedUp);
        
    }

    public void SaveData(ref GameData data)
    {
        if(data.pickedUpItems.ContainsKey(id))
        {
            data.pickedUpItems.Remove(id);
        }

        data.pickedUpItems.Add(id, pickedUp);
    }
}
