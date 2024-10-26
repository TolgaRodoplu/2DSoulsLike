using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TravelButton : MonoBehaviour, IDataPersistance
{
    [HideInInspector] public string bonfireID;
    [HideInInspector] public string bonfireName;
    [HideInInspector] public int scane;
    [HideInInspector] public Vector3 position;
    bool isClicked;

    public void LoadData(ref GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        if (this.isClicked) 
        {
            data.lastBonfire = this.bonfireID;
            isClicked = false;
        }
        
    }

    public void TravelButtonInit(string bonfireID, string bonfireName, int scane, Vector3 position)
    {
        this.bonfireID = bonfireID;
        this.bonfireName = bonfireName;
        this.scane = scane;
        this.position = position;

        Debug.Log(bonfireName);

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.bonfireName;
    }

    public void Travel()
    {
        DataPersistenceManager.instance.GetData().lastBonfire = this.bonfireID; 
        EventSystem.instance.LoadScane(this.scane);
    }
}
