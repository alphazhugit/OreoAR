using UnityEngine;
using System.IO;
public class VideoPlayBehaviour : VideoPlayBase
{
    /// <summary>
    /// 设置视频播放器，编辑器里用UnityVideoPlayer，移动平台用VoidVideoPlayer
    /// </summary>
    void Awake() {
#if UNITY_EDITOR
        var videoPlayer = GetComponent<UnityVideoPlayer>();
        if (videoPlayer == null) {
            videoPlayer = gameObject.AddComponent<UnityVideoPlayer>();
        }
#else
        var videoPlayer = GetComponent<VoidVideoPlayer>();
        if (videoPlayer == null) {
            videoPlayer = gameObject.AddComponent<VoidVideoPlayer>();
        }
#endif
        videoPlayer.AddEventListener(VoidAREvent.READY, OnReady);
        videoPlayer.AddEventListener(VoidAREvent.END, OnEnd);
        videoPlayer.loop = loop;
        vPlayer = videoPlayer;
        
        if (!string.IsNullOrEmpty(url))
        {
            SetPlayerSource(url);
        }
    }

    /// <summary>
    /// 设置播放内容
    /// </summary>
    /// <param name="source"></param>
    public override void SetPlayerSource(string source)
    {
#if UNITY_EDITOR
        if (!source.StartsWith("http"))
        {
            source = Path.Combine(VoidARUtils.GetStreamDirForWWW(), source);
#if UNITY_2017_2_OR_NEWER
            source = source.Replace("file:///", string.Empty);
#endif
        }
#endif
        base.SetPlayerSource(source);
    }

    void OnReady(VoidAREvent evt) {
        OnPrepared();
    }

    void OnEnd(VoidAREvent evt) {

    }
}
