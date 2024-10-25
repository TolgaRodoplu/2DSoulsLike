using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animate : MonoBehaviour
{

    Image image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float animTime = 0.1f;

    private void Start()
    {
        image = GetComponent<Image>();
        
        StartCoroutine(AnimateObj());
    }

    IEnumerator AnimateObj()
    {
        int cnt = 0;

        while (true) 
        {
            
            image.sprite = sprites[cnt];

            cnt++;

            if(cnt == sprites.Length)
                cnt = 0;

            yield return new WaitForSeconds(animTime);
        }
    }
}
