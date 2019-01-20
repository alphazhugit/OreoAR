using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
public class BuildProcessEditor {

    [PostProcessSceneAttribute]
    public static void OnPostprocessScene()
    {
        //如果编辑器在运行状态，则直接返回
        if (EditorApplication.isPlaying)
        {
            return;
        }
        else
        {
            var imageTargets = UnityEngine.Object.FindObjectsOfType<ImageTargetBase>();

            for (int i = 0; i < imageTargets.Length; i++) {
                MeshRenderer renderer = imageTargets[i].GetComponent<MeshRenderer>();
                if (renderer != null) {
                    UnityEngine.Object.DestroyImmediate(renderer);
                }
                
            }
        }
    }
}
