using UnityEngine;
using UnityEngine.Video;
/// <summary>
/// Unity官方播放器，默认在非移动平台中播放视频,也可用在移动平台
/// 注意：Unity VideoPlayer在不同版本的Unity中，可能存在播放视频路径问题（https://forum.unity.com/threads/videoplayer-error-with-2017-3.510384/）
/// </summary>
public class UnityVideoPlayer : VoidAREventBehaviour, IMediaPlayer {
    private VideoPlayer vPlayer;
    public bool loop = false;
    void Awake () {
        vPlayer = GetComponent<VideoPlayer>();
        if (vPlayer == null) {
            vPlayer = gameObject.AddComponent<VideoPlayer>();
        }
        vPlayer.source = VideoSource.Url;
        vPlayer.playOnAwake = true;
        vPlayer.renderMode = VideoRenderMode.APIOnly;
        vPlayer.prepareCompleted += (VideoPlayer vp) => {
            DispatchEvent(VoidAREvent.READY);
        };
        vPlayer.loopPointReached += (VideoPlayer vp) =>
        {
            DispatchEvent(VoidAREvent.END);
            if (loop)
            {
                seek = 0;
                Play();
            }
        };
    }

    public float duration {
        get {
            return (float)vPlayer.frameCount / vPlayer.frameRate;
        }
    }

    public bool isPlaying {
        get {
            return vPlayer.isPlaying;
        }
    }

    public bool isPrepared {
        get {
            return vPlayer.isPrepared;
        }
    }

    public float seek {
        get {
            return (float)vPlayer.time;
        }
        set {
            vPlayer.time = value;
        }
    }

    public string source {
        get {
            return vPlayer.url;
        }
        set {
            vPlayer.url = value;
        }
    }

    public Texture texture {
        get {
            return vPlayer.texture;
        }
    }


    public int videoHeight {
        get {
            return vPlayer.isPrepared ? vPlayer.texture.height : 0;
        }
    }

    public int videoWidth {
        get {
            return vPlayer.isPrepared ? vPlayer.texture.width : 0;
        }
    }
    public float volume {
        get {
            return 1;
        }
        set {
        }
    }

    public bool Pause() {
        vPlayer.Pause();
        return true;
    }

    public bool Play() {
        vPlayer.Play();
        return true;
    }

    public bool Stop()
    {
        vPlayer.Stop();
        return true;
    }
}
