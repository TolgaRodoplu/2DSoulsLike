using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bonfire : Interactable, IDataPersistance
{

    [SerializeField] private string id;

    private int scaneID;

    [ContextMenu("Generate ID")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    Transform spawnPos;

    [SerializeField] GameObject fireObj;

    bool isLit = false;

    private void Start()
    {
        scaneID = SceneManager.GetActiveScene().buildIndex;
    }

    public override void Interact(Transform interactor)
    {
        EventSystem.instance.RestorePlayer();
        DataPersistenceManager.instance.GetData().lastBonfire = this.id;
        DataPersistenceManager.instance.GetData().onBonfire = true;
        if (!isLit)
        {
            fireObj.SetActive(true);
            isLit = true;
            type = InteractionType.Use;
        }
        else
        {
            EventSystem.instance.OpenTeleportUI();
        }

        DataPersistenceManager.instance.SaveGame();
    }

    public void LoadData(ref GameData data)
    {
        if(data.litBonfires.ContainsKey(id))
        {
            type = InteractionType.Use;
            fireObj.SetActive(true);
        }
    }



    public void SaveData(ref GameData data)
    {
        if (!data.litBonfires.ContainsKey(id) && isLit)
        {
            data.litBonfires.Add(id, scaneID);
            data.litBonfireLocations.Add(id, transform.position);
            data.litBonfireNames.Add(id, objectName);
        }

    }
}
