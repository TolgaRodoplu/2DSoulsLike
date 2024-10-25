using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact(Transform interactor);
    public void Uninteract();
}
public enum InteractionType
{
    Light,
    Use,
    Open,
    Close,
    Grab
}