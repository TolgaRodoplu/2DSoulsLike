using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaneTrigger : MonoBehaviour
{
    public TravelType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            int offset = 1;

            if (type == TravelType.Enter)
            {
                offset = -1;
            }

            StaticEnterData.travelType = type;


            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + offset);
        }
    }
}
