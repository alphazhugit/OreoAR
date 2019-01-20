using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class VoidARBuildAssetBundleEditor : EditorWindow
{
    bool[] selectPlatforms = new bool[4] { true, true, true ,true};
    BuildTarget[] buildPlatforms = new BuildTarget[4] { BuildTarget.StandaloneWindows, BuildTarget.StandaloneOSXIntel, BuildTarget.Android, BuildTarget.iOS };
    [MenuItem("VoidAR/AssetBundleBuilder")]
    static void Init()
    {
        VoidARBuildAssetBundleEditor window = (VoidARBuildAssetBundleEditor)EditorWindow.GetWindow(typeof(VoidARBuildAssetBundleEditor));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Select Platforms");
        EditorGUILayout.BeginHorizontal();
        
        selectPlatforms[0] = EditorGUILayout.ToggleLeft("Windows", selectPlatforms[0], GUILayout.MaxWidth(80));
        selectPlatforms[1] = EditorGUILayout.ToggleLeft("OSX", selectPlatforms[1], GUILayout.MaxWidth(80));
        selectPlatforms[2] = EditorGUILayout.ToggleLeft("Android", selectPlatforms[2], GUILayout.MaxWidth(80));
        selectPlatforms[3] = EditorGUILayout.ToggleLeft("iOS", selectPlatforms[3], GUILayout.MaxWidth(80));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("BuildAssetBundles"))
        {
            CreateAssetBunldes();
            //BuildPipeline.BuildAssetBundles(filePath, BuildAssetBundleOptions.None, targetPlatform);
        }

        EndWindows();
    }

    void CreateAssetBunldes()
    {
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            for (int i = 0; i < buildPlatforms.Length; i++) {
                if (selectPlatforms[i] == true) {
                    BuildAssetBundle(buildPlatforms[i],obj);
                }
            }
        }
        AssetDatabase.Refresh();

    }

    void BuildAssetBundle(BuildTarget targetPlatform,Object targetObject)
    {
        string outputPath = GetBuildOutPutPath(targetPlatform);
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        string targetPath = outputPath + targetObject.name + ".assetbundle";
        if (BuildPipeline.BuildAssetBundle(targetObject, null, targetPath, BuildAssetBundleOptions.CollectDependencies, targetPlatform))
        {
            Debug.Log(targetObject.name + "assetbundle [" + targetPlatform.ToString() + "]制作成功");
        }
        else
        {
            Debug.Log(targetObject.name + "assetbundle [" + targetPlatform.ToString() + "]制作成功");
        }
    }

    string GetBuildOutPutPath(BuildTarget buildTarget) {
        string path = "";
        switch (buildTarget)
        {
            case BuildTarget.Android:
                path = Application.streamingAssetsPath + "/Assetbundle/Android/";
                break;
            case BuildTarget.iOS:
                path = Application.streamingAssetsPath + "/Assetbundle/iOS/";
                break;
            case BuildTarget.StandaloneWindows:
                path = Application.streamingAssetsPath + "/Assetbundle/Windows/";
                break;
           // case BuildTarget.StandaloneOSX:
           //     path = Application.streamingAssetsPath + "/Assetbundle/OSX/";
           //     break;
        }
        return path;
    }
}