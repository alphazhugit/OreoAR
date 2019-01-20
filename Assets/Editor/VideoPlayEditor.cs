using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(VideoPlayBehaviour))]
public class VideoPlayEditor : Editor
{
    void Awake()
    {
        EditorApplication.update += update;
    }

    void update()
    {
        if (EditorApplication.isPlaying)
            return;
        var videoPlay = (VideoPlayBehaviour)target;
        if (videoPlay.autoScale)
        {
            Material mat = videoPlay.transform.parent.GetComponent<MeshRenderer>().sharedMaterial;
            if (mat != null)
            {
                float ratio = (float)mat.mainTexture.height / (float)mat.mainTexture.width;
                if (ratio < 1f)
                {
                    videoPlay.transform.localScale = new Vector3((1.0f / ratio) / 10f, 1.0f / 10f, 1.0f / 10f);
                }
                else
                {
                    videoPlay.transform.localScale = new Vector3(1.0f / 10f, 1.0f / 10f, ratio / 10f);
                }  
                videoPlay.transform.localPosition = Vector3.zero;
            }
        }
    }

    void OnDestroy() {
        EditorApplication.update -= update;
    }
}