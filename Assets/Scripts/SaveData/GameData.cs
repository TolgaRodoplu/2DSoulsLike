using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{

    public float playerHealth;

    public int currentScene;

    public Vector3 playerPos;

    public float healthBarFillAmounth;

    public SerializableDictionary<string, string> litBonfireNames;

    public SerializableDictionary<string, Vector3> litBonfireLocations;

    public SerializableDictionary<string, int> litBonfires;

    public SerializableDictionary<int, string> inventoryItems;

    public SerializableDictionary<string, bool> pickedUpItems;

    public string lastBonfire;

    public GameData() 
    {
        playerHealth = 100;
        currentScene = 1;
        playerPos = new Vector3(-2.5f, -0.7f, 0);
        litBonfireLocations = new SerializableDictionary<string, Vector3>();
        litBonfireNames = new SerializableDictionary<string, string>();
        litBonfires = new SerializableDictionary<string, int>();
        inventoryItems = new SerializableDictionary<int, string>();
        pickedUpItems = new SerializableDictionary<string, bool>();
        lastBonfire = string.Empty; 
    }

}

