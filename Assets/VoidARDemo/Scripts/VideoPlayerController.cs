using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 视频全屏播放带播放控制器
/// </summary>
public class VideoPlayerController : MonoBehaviour {
    public ImageTargetBehaviour imageTargetBehaviour;
    public string videoSource;
    public RawImage rawImage;
    public Sprite playImage;
    public Sprite pauseImage;
    public Button playButton;
    public Text currTimeText;
    public Text durationText;
    public Image progressBar;
    private GameObject videoObject;
    private int mDuration = 0;
    private IMediaPlayer vPlayer;
    void Awake () {
        imageTargetBehaviour.AddEventListener(VoidAREvent.FIND, OnFind);
        videoObject = rawImage.gameObject;
        playButton.onClick.AddListener(OnPlayButtonClickHandler);
        var mVideoProgressBar = progressBar.GetComponent<VideoProgressBar>();
        mVideoProgressBar.onValueChanged = OnValueChanged;
        playButton.transform.parent.gameObject.SetActive(false);
#if UNITY_EDITOR
        var videoPlayer = gameObject.AddComponent<UnityVideoPlayer>();
#else
        var videoPlayer = gameObject.AddComponent<VoidVideoPlayer>();
#endif
        videoPlayer.AddEventListener(VoidAREvent.READY, OnReady);
        videoPlayer.AddEventListener(VoidAREvent.END, OnComplete);
        vPlayer = videoPlayer;
        rawImage.enabled = false;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rawImage.rectTransform.localScale = new Vector3(1, -1, 1);
        }
    }

    void OnReady(VoidAREvent evt)
    {
        rawImage.texture = vPlayer.texture;
        rawImage.enabled = true;
        playButton.transform.parent.gameObject.SetActive(true);
        playButton.GetComponent<Image>().sprite = pauseImage;
        rawImage.GetComponent<AspectRatioFitter>().aspectRatio = (float)vPlayer.videoWidth / vPlayer.videoHeight;
        mDuration = (int)vPlayer.duration;
        durationText.text = GetTime(mDuration);
    }

    void OnComplete(VoidAREvent evt)
    {
        playButton.GetComponent<Image>().sprite = playImage;
    }

    void OnPlayButtonClickHandler() {
        if (rawImage.enabled)
        {
            if (vPlayer.isPlaying)
            {
                vPlayer.Pause();
                playButton.GetComponent<Image>().sprite = playImage;
            }
            else {
                vPlayer.Play();
                playButton.GetComponent<Image>().sprite = pauseImage;
            }
        }
    }

    void OnFind(VoidAREvent evt) {
        if(mDuration == 0)
            PlayVideo(videoSource);
    }

    public void PlayVideo(string url) {
#if UNITY_EDITOR
        if (!url.StartsWith("http"))
        {
            url = Path.Combine(VoidARUtils.GetStreamDirForWWW(), url);
        }
#endif
        vPlayer.source = url;
        vPlayer.Play();
    }

    void OnValueChanged(float val) {
        vPlayer.seek = mDuration * val;
        if (!vPlayer.isPlaying) {
            OnPlayButtonClickHandler();
        }
    }

    void Update()
    {
        if (vPlayer.isPlaying)
        {
            if (mDuration > 0)
            {
                currTimeText.text = GetTime((int)(vPlayer.seek));
                progressBar.fillAmount = vPlayer.seek / mDuration;
            }
        }
    }

    private string GetTime(int time)
    {
        TimeSpan result = TimeSpan.FromSeconds(time);
        if (result.Hours > 0)
        {
            return string.Format("{0:D2}:{1:D2}m:{2:D2}",
                result.Hours,
                result.Minutes,
                result.Seconds);
        }
        else {
            return string.Format("{0:D2}:{1:D2}",
                result.Minutes,
                result.Seconds);
        }
    }
}