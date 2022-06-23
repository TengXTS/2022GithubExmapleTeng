using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class WhiteLightFade : MonoBehaviour
{
    private Image whiteLight;
    private Color c;

    private void Start()
    {
        whiteLight = GameObject.Find("whiteLight").GetComponent<Image>();
        c = new Color(255,255,255,1f);
    }


    void Update()
    {
        StartCoroutine(whiteLightFade());

        if (whiteLight.color.a <= 0)
        {
            Destroy(gameObject);
        }

        IEnumerator whiteLightFade()
        {
            
            c.a -= 0.04f;
            whiteLight.color = c;
            // Debug.Log("scene2"+c.a);
            yield return null;
        }
    }
}
