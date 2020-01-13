using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class VideoPlayButtonEffect : MonoBehaviour {
    public Sprite playImage;
    public Sprite pauseImage;
    private VideoPlayBehaviour videoPlayBehaviour;
    public SpriteRenderer buttonRenderer;
    private Vector3 finalScale;
    void Awake () {
        videoPlayBehaviour = GetComponent<VideoPlayBehaviour>();
        finalScale = buttonRenderer.transform.localScale;
    }

    void OnMouseDown()
    {
        buttonRenderer.sprite = videoPlayBehaviour.vPlayer.isPlaying ? pauseImage : playImage;
        if (videoPlayBehaviour.vPlayer.isPlaying)
        {
            videoPlayBehaviour.vPlayer.Pause();
        }
        else
        {
            videoPlayBehaviour.vPlayer.Play();
        }
        StartCoroutine(EffectCoroutine(0.8f));
    }

    IEnumerator EffectCoroutine(float duration)
    {
        float start = Time.time;
        while (Time.time <= start + duration)
        {
            float val = Mathf.Clamp01((Time.time - start) / duration);  //0-1
            Color color = buttonRenderer.color;
            color.a = 1f - val;
            buttonRenderer.color = color;
            buttonRenderer.transform.localScale = finalScale * val;
            yield return new WaitForEndOfFrame();
        }
    }
}