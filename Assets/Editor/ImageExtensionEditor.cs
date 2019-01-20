using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;


[CustomEditor(typeof(ImageExtensionBehaviour))]
[InitializeOnLoad]
public class ImageExtensionEditor : Editor
{
    static Dictionary<string, Material> matMap = new Dictionary<string, Material>();

    static ImageExtensionEditor()
    {
        EditorApplication.playmodeStateChanged += () => {
            if (EditorApplication.isPlayingOrWillChangePlaymode == false && EditorApplication.isPlaying == false)
            {
                ImageExtensionBehaviour[] markers = GameObject.FindObjectsOfType<ImageExtensionBehaviour>();
                foreach (ImageExtensionBehaviour marker in markers)
                {
                    var render = marker.GetComponent<MeshRenderer>();
                    if (render != null)
                    {
                        render.enabled = true;
                    }
                }
            }
        };

        EditorApplication.hierarchyWindowChanged += () =>
        {
            ImageExtensionBehaviour[] markers = GameObject.FindObjectsOfType<ImageExtensionBehaviour>();
            foreach (ImageExtensionBehaviour marker in markers)
            {
                ImageExtensionBehaviour tmp = marker;
                UpdatePreView(ref tmp);
            }
        };
    }

    SerializedProperty widthProp;
    void OnEnable()
    {
        widthProp = serializedObject.FindProperty("MarkerWidth");
        Debug.Log("widthProp = " + widthProp);
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        //Debug.Log("OnInspectorGUI");
        ImageExtensionBehaviour marker = target as ImageExtensionBehaviour;
        serializedObject.Update();
        EditorGUILayout.PropertyField(widthProp);
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(marker);
            UpdatePreView(ref marker);
        }
    }

    private static void UpdatePreView(ref ImageExtensionBehaviour marker)
    {
        if (Application.isPlaying)
            return;


        var mat = GetMarkerMaterial(Application.streamingAssetsPath + "/" + marker.GetPath());
        if (mat)
        {
            var render = marker.GetComponent<MeshRenderer>();
            if (render == null)
            {
                render = marker.gameObject.AddComponent<MeshRenderer>();
            }
            render.material = mat;
            float ratio = (float)mat.mainTexture.height / (float)mat.mainTexture.width;
            var meshFilter = marker.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                meshFilter = marker.gameObject.AddComponent<MeshFilter>();
            }
            meshFilter.sharedMesh = makeQuadMesh(ratio);
        }
        EditorUtility.UnloadUnusedAssetsImmediate();
    }

    private static Mesh makeQuadMesh(float ratio)
    {
        var mesh = new Mesh();
        mesh.Clear();
        Vector3[] vertices = new Vector3[4];

        vertices[0] = new Vector3(-0.5f, 0f, -ratio * 0.5f);
        vertices[1] = new Vector3(-0.5f, 0f, ratio * 0.5f);
        vertices[2] = new Vector3(0.5f, 0f, -ratio * 0.5f);
        vertices[3] = new Vector3(0.5f, 0f, ratio * 0.5f);

        Vector2[] uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(1, 1),
        };

        int[] triangles = new int[] {
            0, 1, 2,
            2, 1, 3,
        };
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        ;
        return mesh;
    }

    public static Material GetMarkerMaterial(string filePath)
    {

        Material mat = null;
        byte[] fileData;

        if (matMap.ContainsKey(filePath) && matMap[filePath] != null)
        {
            return matMap[filePath];
        }
        else
        {
            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                Texture2D tex = new Texture2D(2, 2);

                tex.LoadImage(fileData);
                mat = new Material(Shader.Find("Unlit/Texture"));
                mat.mainTexture = tex;
                matMap[filePath] = mat;
            }
            return mat;
        }
    }
}
