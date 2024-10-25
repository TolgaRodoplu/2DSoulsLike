using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Item", order = 1)]

public class Item : ScriptableObject
{
    public Sprite sprite;

    [SerializeField] public string id;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
