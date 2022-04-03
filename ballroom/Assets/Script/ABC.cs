using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class ABC : MonoBehaviour
{
    Mesh originMesh;
    public Vector3[] verticesPosition;
    
 
    class Bone
    {
        internal Transform bone;
        internal float weight;
        internal Vector3 delta;
    }
    List<List<Bone>> allBones = new List<List<Bone>>();
 
    void Start()
    {
        SkinnedMeshRenderer skin = GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
        originMesh = skin.sharedMesh;
        verticesPosition = new Vector3[originMesh.vertexCount];
        // Debug.Log("{0} vertices, {1} weights, {2} bones"+ mesh.vertexCount+ mesh.boneWeights.Length+ skin.bones.Length);
 
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
 
                    bone.bone = skin.bones[boneIndices[j]];
                    bone.weight = boneWeights[j];
                    bone.delta = bone.bone.InverseTransformPoint(position);
                }
            }
            
        }
    }


    private void Update()
    {
        for (int i = 0; i < originMesh.vertexCount; i++)
        {
            List<Bone> bones = allBones[i];

            Vector3 position = Vector3.zero;
            foreach (Bone bone in bones)
                position += bone.bone.TransformPoint(bone.delta) * bone.weight;
            verticesPosition[i] = position;
        }
    }


}
//This script comes from:
//https://forum.unity.com/threads/get-skinned-vertices-in-real-time.15685/
