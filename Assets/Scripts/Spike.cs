using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Coroutine coroutine = null;
    public float damageToDeal = -10;
    public float damageFreq = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            coroutine = StartCoroutine(Hurt());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(coroutine);
        }
    }

    IEnumerator Hurt()
    {
        while (true) 
        {
            EventSystem.instance.EditHealth(damageToDeal);
            yield return new WaitForSeconds(damageFreq);
        }
        
    }
}
