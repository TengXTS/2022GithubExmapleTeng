using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

 
public class SkinnedVertices : MonoBehaviour
{
    Mesh originMesh;
    // public Vector3[] verticesPosition;
    public List<Vector3> verticesPosition;
    public List<Vector3> verticesPositionAfterDelete;


    class Bone
    {
        internal Transform boneTransform;
        internal float weight;
        internal Vector3 delta;
    }
    List<List<Bone>> allBones = new List<List<Bone>>();
    
    void Start()
    {
        SkinnedMeshRenderer skin = GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        originMesh = skin.sharedMesh;
        // verticesPosition = new Vector3[originMesh.vertexCount];
        verticesPosition = new List<Vector3>();
 
    for (int i = 0; i < originMesh.vertexCount; i++)
    {
        Vector3 position = originMesh.vertices[i];
        position = transform.TransformPoint(position);
    
        BoneWeight weights = originMesh.boneWeights[i];
        int[] boneIndices = new int[] { weights.boneIndex0, weights.boneIndex1, weights.boneIndex2, weights.boneIndex3 };
        float[] boneWeights = new float[] { weights.weight0, weights.weight1, weights.weight2, weights.weight3 };
    
        List<Bone> bones = new List<Bone>();
        allBones.Add(bones);
    
        for (int j = 0; j < 4; j++)
        {
            if (boneWeights[j] > 0)
            {
                Bone bone = new Bone();
                bones.Add(bone);
    
                bone.boneTransform = skin.bones[boneIndices[j]];
                bone.weight = boneWeights[j];
                bone.delta = bone.boneTransform.InverseTransformPoint(position);
            }
        }
        verticesPosition.Add(position);
        verticesPositionAfterDelete = verticesPosition.Distinct().ToList();

    }
  
    }


     private void Update()
     {
         for (int i = 0; i < originMesh.vertexCount; i++)
         {
             List<Bone> bones = allBones[i];

             Vector3 position = Vector3.zero;
             foreach (Bone bone in bones)
                 position += bone.boneTransform.TransformPoint(bone.delta) * bone.weight;
             verticesPosition[i] = position;
         }
         verticesPositionAfterDelete = verticesPosition.Distinct().ToList();
        
     }


}
//This script comes from:
//https://forum.unity.com/threads/get-skinned-vertices-in-real-time.15685/
