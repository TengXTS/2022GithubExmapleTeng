using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class SkinnedVertices : MonoBehaviour
{
    Mesh mesh;
    // public List<Vector3> verticesPosition = new List<Vector3>();
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
        mesh = skin.sharedMesh;
        verticesPosition = new Vector3[mesh.vertexCount];
        // Debug.Log("{0} vertices, {1} weights, {2} bones"+ mesh.vertexCount+ mesh.boneWeights.Length+ skin.bones.Length);
 
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            Vector3 position = mesh.vertices[i];
            position = transform.TransformPoint(position);
 
            BoneWeight weights = mesh.boneWeights[i];
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
 
            //if (bones.Count > 1)
            //{
            //    string msg = string.Format("vertex {0}, {1} bones", i, bones.Count);
 
            //    foreach (Bone bone in bones)
            //        msg += string.Format("\n\t{0} => {1} => {2}", bone.bone.name, bone.weight, bone.delta);
 
            //    Debug.Log(msg);
            //}
        }
    }


    private void Update()
    {
        for (int i = 0; i < mesh.vertexCount; i++)
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