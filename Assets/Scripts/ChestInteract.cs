using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    public List<Item> chestInventory = new List<Item>();
    Animator anim;
    bool isOpen = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void Interact(Transform interactor)
    {
        Debug.Log("Open");
        isOpen = true;
        anim.SetBool("IsOpened", isOpen);
    }
}
