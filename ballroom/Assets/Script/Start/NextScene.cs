using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class NextScene : MonoBehaviour
{
    public float inflateSpeed = 0.01f;
    private bool enter = false;

    private Image whiteLight;

    private Color c;
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
            // Debug.Log("scene1"+c.a);
            this.GetComponent<Transform>().localScale = this.GetComponent<Transform>().localScale + new Vector3(inflateSpeed,inflateSpeed,inflateSpeed);

            
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene("Main");
    
        }


    }
    
    

}
