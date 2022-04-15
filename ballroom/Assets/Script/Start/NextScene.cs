using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    private bool enter = false;

    private Image whiteLight;

    private Color c;
    // image
    void Start()
    {
        whiteLight = GameObject.Find("whiteLight").GetComponent<Image>();
        c = new Color(255,255,255,-1f);


    }

    // Update is called once per frame
    void Update()
    {

            StartCoroutine(ExampleCoroutine());

    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("0"+ enter);

        enter = true;
        
    }
    IEnumerator ExampleCoroutine()
    {
        

        if (enter == true)
        {
            c.a += 0.04f;
            whiteLight.color = c;
            // Debug.Log(c.a);
            this.GetComponent<Transform>().localScale = this.GetComponent<Transform>().localScale + new Vector3(0.01f,0.01f,0.01f);

            

            // Debug.Log("1"+ enter);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Main");
        }


    }
    
    

}
