using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System.Linq;

// using Image = UnityEngine.UI.Image;

//待办清单

//main场景资产制作：场景建模，元素
//main场景shader编写：镜子，水面，云层，（特效）
//avatar要不要一直在上下浮动呢
//元素间渐变（接触到某物，然后元素扩散式变化，过渡效果1：变大）
//开头:摄像头变换，地面纹理，光柱细节用贴图做，空中灰尘,直视光源的光圈，第三人称
//按键教程文字
//元素位置随机
//avatar移动测试（可将slider映射到键盘上）
//一个新想法：要不要让avatar的视角不freeze，可设置一个键来归零防止转晕


//各种设置指南：
//导入新avatar模型时注意：在import/mesh设置中开启read/write；gameobject及各关节命名要与目前的保持一致；取消c4d摄像头
//element使用的material要开启GPU instance，element的gameobject要全部拖入本脚本的elementList

public class Main : MonoBehaviour
{

    private float[] fingers;
    private PublicFunctions publicFunctions;
 
    private float moveTolerance = 1;

    //手脚转动起止角度
    private Quaternion leftHandStartAngle = new Quaternion(0.00048f, 0.78203f, 0.62324f, -0.00103f);
    private Quaternion leftHandEndAngle = new Quaternion(-0.00039f, 0.99336f, -0.11502f, -0.00106f);
    private Quaternion rightHandStartAngle = new Quaternion(-0.54521f, -0.00081f, -0.00053f, 0.83830f);
    private Quaternion rightHandEndAngle = new Quaternion(0.18873f, -0.00095f, 0.00018f, 0.98203f);
    private Quaternion leftHipStartAngle = new Quaternion(-0.40338f, 0.45920f, 0.73886f, -0.28373f);
    private Quaternion leftHipEndAngle = new Quaternion(0.32134f, 0.50926f, 0.77806f, 0.17896f);
    private Quaternion rightHipStartAngle = new Quaternion(-0.48733f, -0.40875f, 0.23933f, 0.73359f);

    private Quaternion rightHipEndAngle = new Quaternion(-0.47522f, 0.31772f, -0.21946f, 0.79061f);
    // private Quaternion leftKneeStartAngle = new Quaternion(0.00059f, -0.00002f, 0.00000f, 1.00000f);
    // private Quaternion leftKneeEndAngle = new Quaternion(0.00056f, -0.31499f, -0.00019f, 0.94909f);

    [Header("元素外观")] public float meshScale = 0.1f;
    [Range(0.0f, 1.0f)] public float scaleRandom = 0.3f;
    [Range(0.0f, 180.0f)] public float rotationRandom = 45;

    //instance相关
    [Header("初始元素")]public Mesh mesh;
    public List<GameObject> elementList = new List<GameObject>();
    public Material material;
    private Matrix4x4[] matrices;
    private MaterialPropertyBlock block;
    private int verticesAmount;
    private List<Vector3> verticesPosition = new List<Vector3>();
    private float sliderLength;

    // private GameObject leftHand;
    private GameObject leftShoulder;

    // private Transform leftHandTransform;
    private Transform leftShoulderTransform;
    private Vector3 leftShoulderPosition;

    // private GameObject rightHand;
    private GameObject rightShoulder;

    // private Transform rightHandTransform;
    private Transform rightShoulderTransform;
    private Vector3 rightShoulderPosition;

    private float leftHandAngle;
    private float rightHandAngle;

    private GameObject leftHip;
    private GameObject leftKnee;
    private Transform leftHipTransform;

    private GameObject rightHip;
    private GameObject rightKnee;
    private Transform rightHipTransform;


    private GameObject bodyMesh;
    private float bodyRotateValue;
    private Quaternion[] rotation;
    private Vector3[] scale;

    
    public static float Remap ( float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    void Start()
    {

        publicFunctions = GameObject.Find("Script").GetComponent<PublicFunctions>();
        fingers = publicFunctions.fingers;
        sliderLength = publicFunctions.sliderLength;
        moveTolerance = publicFunctions.moveTolerance;
        
        
        leftShoulder = GameObject.Find("leftShoulder");
        leftShoulderTransform = leftShoulder.GetComponent<Transform>();
        
        rightShoulder = GameObject.Find("rightShoulder");
        rightShoulderTransform = rightShoulder.GetComponent<Transform>();
        
        leftHip = GameObject.Find("leftHip");
        leftHipTransform = leftHip.GetComponent<Transform>();
        // leftKnee = GameObject.Find("leftKnee");
        
        rightHip = GameObject.Find("rightHip");
        rightHipTransform = rightHip.GetComponent<Transform>();
        // rightKnee = GameObject.Find("rightKnee");
        
  
        //手指中位
        for (int i = 0; i < 10; i++)
        {
            fingers[i] = sliderLength / 2;
        }

        bodyMesh = GameObject.Find("bodyMesh");
        block = new MaterialPropertyBlock();
        verticesAmount = bodyMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh.vertexCount;
        
        //简化顶点个数
        verticesPosition = bodyMesh.GetComponent<SkinnedVertices>().verticesPositionAfterDelete;
        verticesAmount = verticesPosition.Count;
        matrices = new Matrix4x4[verticesAmount];
        //计算每个element的大小与旋转
        rotation = new Quaternion[verticesAmount];
        scale = new Vector3[verticesAmount];
        for (int i = 0; i < verticesAmount; i++)
        {
            rotation[i] = Quaternion.Euler(Random.Range(-rotationRandom, rotationRandom), Random.Range(-rotationRandom, rotationRandom), Random.Range(-rotationRandom, rotationRandom));
            scale[i] = Vector3.one * Random.Range(1-scaleRandom, 1+scaleRandom);
        }
        //element标记物碰撞检测
        foreach (var element in elementList)
        {
            element.AddComponent<TriggerDetact>();
            element.GetComponent<Collider>().isTrigger = true;
        }


    }
    
    

    void Update()
    {
  
            LimbControl();
            publicFunctions.Move("Float");
            publicFunctions.Rotate();
            AddElements();
        
        
     
    }


    private void AddElements()
    {
        //元素排列
        verticesPosition = bodyMesh.GetComponent<SkinnedVertices>().verticesPositionAfterDelete;
        for (int i = 0; i < verticesAmount; i++)
        {
            matrices[i] = Matrix4x4.TRS(verticesPosition[i], rotation[i], scale[i] * meshScale);
        }

        bool anyCollision = false;

        //元素切换
        foreach (var element in elementList)
        {
            string colliderName = element.GetComponent<TriggerDetact>().colliderName;
            bool ifCollision = element.GetComponent<TriggerDetact>().ifCollision;//只有最后一个element的值是有效的。我想要的：任意一个为真则为真
            if (colliderName != null)
            {
                // Debug.Log(colliderName);
                //目前暂时是变成和标记物一样的东西，之后可能需要不一样的
                mesh = GameObject.Find(colliderName).GetComponent<MeshFilter>().mesh;
            }
            //检测是否有任何碰撞发生
            if (ifCollision == true)
            {
                anyCollision = true;
            }
        }

        if (anyCollision == true)
        {
            Graphics.DrawMeshInstanced(mesh, 0, material, matrices, verticesAmount, block);
        }
//使用两个draw，先试试上下扫描变换
//矩阵中，去掉y大的位置，阈值随时间变化
    }
    
   


    void LimbControl()
    {
        //最好增加一些变化
        
        leftShoulderTransform.localRotation = Quaternion.Slerp(leftHandStartAngle, leftHandEndAngle, fingers[6] / sliderLength);
        rightShoulderTransform.localRotation = Quaternion.Slerp(rightHandStartAngle, rightHandEndAngle, fingers[9] / sliderLength);
        leftHipTransform.localRotation = Quaternion.Slerp(leftHipStartAngle, leftHipEndAngle, fingers[7] / sliderLength);
        rightHipTransform.localRotation = Quaternion.Slerp(rightHipStartAngle, rightHipEndAngle, fingers[8] / sliderLength);
        //膝盖
        // if (fingers[7] > sliderLength / 2)
        // {
        //     leftKnee.GetComponent<Transform>().localRotation = Quaternion.Slerp(leftKneeStartAngle, leftKneeEndAngle,
        //         Remap(fingers[7], sliderLength / 2, sliderLength, 0, 1));
        // }
        


        // Debug.Log(leftKnee.GetComponent<Transform>().localRotation);
        
        
    }


}





