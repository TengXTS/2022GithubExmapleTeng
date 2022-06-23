using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AvatarControl : MonoBehaviour
{

    private float[] fingers;
    private PublicFunctions publicFunctions;
    private float moveTolerance = 1;
    private float sliderLength;
    private GameObject Myavatar;
    private Transform MyavatarTransform;

    void Start()
    {
        Myavatar = GameObject.Find("avatar");
        MyavatarTransform = Myavatar.GetComponent<Transform>();
        publicFunctions = GameObject.Find("Script").GetComponent<PublicFunctions>();
        fingers = publicFunctions.fingers;
        sliderLength = publicFunctions.sliderLength;
        moveTolerance = publicFunctions.moveTolerance;
        
  
        //手指中位
        for (int i = 0; i < 10; i++)
        {
            fingers[i] = sliderLength / 2;
        }
    }
    void Update()
    {
            publicFunctions.Move("Float");
            publicFunctions.Rotate();
            StartCoroutine(ExampleCoroutine());
    }
    IEnumerator ExampleCoroutine()
    {
        if (35 < MyavatarTransform.position.z && MyavatarTransform.position.z < 38 && -1.2f < MyavatarTransform.position.x && MyavatarTransform.position.x < -0.3f)
        {
            Debug.Log("1111");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("Ballroom1");
        }
    }
    


}





