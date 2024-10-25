using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] protected string soundEffect = null;
    [SerializeField] protected string objectName = null;
    public InteractionType type;
    public virtual void Interact(Transform interactor)
    {

    }
    public virtual void Uninteract()
    {

    }


}