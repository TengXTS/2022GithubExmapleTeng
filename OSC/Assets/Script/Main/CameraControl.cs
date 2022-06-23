using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject Myavatar;
    private Transform MyavatarTransform;
    void Start()
    {
        Myavatar = GameObject.Find("avatar");
        MyavatarTransform = Myavatar.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().position = new Vector3(MyavatarTransform.position.x, MyavatarTransform.position.y + 2,
            MyavatarTransform.position.z);
    }
}
