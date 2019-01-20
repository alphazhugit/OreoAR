using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;


[CustomEditor(typeof(ImageTargetBehaviour))]
[InitializeOnLoad]
public class MarkerEditor : Editor
{
    static Dictionary<string, Material> matMap = new Dictionary<string, Material>();

    static MarkerEditor()
    {
        EditorApplication.playmodeStateChanged += () => {
            if (EditorApplication.isPlayingOrWillChangePlaymode == false && EditorApplication.isPlaying == false)
            {
                ImageTargetBehaviour[] markers = GameObject.FindObjectsOfType<ImageTargetBehaviour>();
                foreach (ImageTargetBehaviour marker in markers)
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
            ImageTargetBehaviour[] markers = GameObject.FindObjectsOfType<ImageTargetBehaviour>();
            foreach (ImageTargetBehaviour marker in markers)
            {
                ImageTargetBehaviour tmp = marker;
                UpdatePreView(ref tmp);
            }
        };
    }

    SerializedProperty widthProp;
    void OnEnable()
    {
        widthProp = serializedObject.FindProperty("MarkerWidth");
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        //Debug.Log("OnInspectorGUI");
        if (GUI.changed)
        {
            ImageTargetBehaviour marker = target as ImageTargetBehaviour;
            EditorUtility.SetDirty(marker);
            UpdatePreView(ref marker);
       }
    }

    private static void UpdatePreView(ref ImageTargetBehaviour marker)
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

		if (ratio < 1f)
        {

			float r = 1f / ratio;
			vertices[0] = new Vector3(-r * 0.5f, 0f, -0.5f);
			vertices[1] = new Vector3(-r * 0.5f, 0f, 0.5f);
			vertices[2] = new Vector3(r * 0.5f, 0f, -0.5f);
			vertices[3] = new Vector3(r * 0.5f, 0f, 0.5f);


        }
        else
        {

			vertices[0] = new Vector3(-0.5f, 0f, -ratio * 0.5f);
			vertices[1] = new Vector3(-0.5f, 0f, ratio * 0.5f);
			vertices[2] = new Vector3(0.5f, 0f, -ratio * 0.5f);
			vertices[3] = new Vector3(0.5f, 0f, ratio * 0.5f);

        }

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

    [MenuItem("VoidAR/MarkerBuild")]
    static void Init()
    {
        foreach (var obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            string assetsPath = AssetDatabase.GetAssetPath(obj);
            if (Path.GetExtension(assetsPath) == ".jpg") {
                //VoidAR.GetInstance().saveImageTargetDescriptor(assetsPath);
            }
        }
        AssetDatabase.Refresh();
    }
}
