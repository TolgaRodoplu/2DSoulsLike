using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaneTrigger : MonoBehaviour, IDataPersistance
{
    public bool isEnterance;


    bool didTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           

            int offset = 1;

            if (isEnterance)
            {
                offset = -1;
            }

            didTrigger = true;


            EventSystem.instance.LoadScane(SceneManager.GetActiveScene().buildIndex + offset);
        }
    }

    public void LoadData(ref GameData data)
    {
    }

    public void SaveData(ref GameData data)
    {
        if (didTrigger) 
        {
            data.didWalk = true;
            data.isEnterence = this.isEnterance;
        }

        didTrigger = false;
    }
}

